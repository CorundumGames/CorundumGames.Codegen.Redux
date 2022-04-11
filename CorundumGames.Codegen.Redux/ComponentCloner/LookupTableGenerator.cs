using System;
using System.Linq;
using Entitas;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using CorundumGames.CodeGeneration.Plugins.PreProcessors;
using DesperateDevs.Roslyn;
using Entitas.CodeGeneration.Attributes;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CorundumGames.CodeGeneration.Plugins.ComponentCloner
{
    [PublicAPI]
    public sealed class LookupTableGenerator : ICodeGenerator, ICachable
    {
        public string name => "Component Cloner Table Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            try
            {
                return data
                    .OfType<ComponentData>()
                    .Where(d => d.GetSymbol().GetAttribute<DontGenerateAttribute>() == null)
                    .Aggregate(new Dictionary<string, List<ComponentData>>(), (dict, d) =>
                    {
                        var contextNames = d.GetContextNames();
                        foreach (var contextName in contextNames)
                        {
                            if (!dict.ContainsKey(contextName))
                            {
                                dict.Add(contextName, new List<ComponentData>());
                            }

                            dict[contextName].Add(d);
                        }

                        return dict;
                    })
                    .Select(d => GenerateClonerLookupTable(d.Key, d.Value))
                    .ToArray();
            }
            catch (NullReferenceException)
            { // HACK: Remove this catch and fix the damned NRE (if it even still exists)
                return Array.Empty<CodeGenFile>();
            }
        }

        private CodeGenFile GenerateClonerLookupTable(string contextName, IReadOnlyList<ComponentData> data)
        {
            var project = objectCache[ExposeRoslynProject.ProjectKey] as Project;

            Debug.Assert(project != null);
            var generator = SyntaxGenerator.GetGenerator(project);

            var className = $"{contextName}ComponentCloners";

            var @class = generator.ClassDeclaration(
                name: className,
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Static,
                members: CreateClonerFields(generator, contextName, data)
            );

            var code = generator.CompilationUnit(@class);

            return new CodeGenFile(
                fileContent: code.NormalizeWhitespace().ToFullString(),
                fileName: Path.Combine(contextName, $"{className}.cs"),
                generatorName: GetType().FullName
            );
        }

        private IEnumerable<SyntaxNode> CreateClonerFields(
            SyntaxGenerator generator,
            string contextName,
            IReadOnlyList<ComponentData> data
        )
        {
            var nodes = new List<SyntaxNode>();

            var compilation = objectCache[ExposeRoslynProject.CompilationKey] as Compilation;

            //  public delegate void ${Context}EntityCloner(${Context}Entity entity, IComponent component);

            var entityType = compilation.GetTypeByMetadataName(contextName.AddEntitySuffix());
            var componentType = compilation.GetTypeByMetadataName(typeof(IComponent).FullName);

            var delegateTypeName = generator.IdentifierName($"{contextName.AddEntitySuffix()}Cloner");
            var delegateDeclaration = generator.DelegateDeclaration(
                name: delegateTypeName.ToString(),
                parameters: new[]
                {
                    generator.ParameterDeclaration("entity", generator.TypeExpression(entityType)),
                    generator.ParameterDeclaration("component", generator.TypeExpression(componentType, true))
                },
                accessibility: Accessibility.Public
            );

            nodes.Add(delegateDeclaration);

            // public static ${Context}EntityCloner[] cloners = new...
            var clonerArray = generator.FieldDeclaration(
                name: "cloners",
                type: generator.ArrayTypeExpression(delegateTypeName),
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Static | DeclarationModifiers.ReadOnly,
                initializer: generator.ArrayCreationExpression(delegateTypeName, data
                    .OrderBy(d => d.GetTypeName())
                    .Select(d => CreateCloner(generator, d))
                )
            );

            nodes.Add(clonerArray);

            return nodes;
        }

        private SyntaxNode CreateCloner(SyntaxGenerator generator, ComponentData data)
        {
            // If this is a flag component...
            var prefix = data.GetFlagPrefix();

            var entity = generator.ParameterDeclaration("entity");
            var component = generator.ParameterDeclaration("component");

            // (e, c) => something();
            return generator.VoidReturningLambdaExpression(
                lambdaParameters: new[] { entity, component },
                statements: CreateClonerBody(generator, data)
            );
        }

        private IEnumerable<SyntaxNode> CreateClonerBody(SyntaxGenerator generator, ComponentData component)
        {
            var entity = generator.IdentifierName("entity");
            var members = component.GetMemberData();
            if (members.Length == 0)
            { // If this is a flag component...
                var prefix = component.GetFlagPrefix();

                // (e, c) => e.isFlag = true;
                return new[]
                {
                    generator.AssignmentStatement(
                        left: generator.MemberAccessExpression(entity,
                            $"{prefix}{component.ComponentName()}"),
                        right: generator.TrueLiteralExpression()
                    ),
                };
            }

            var castedComponentDeclaration = generator.LocalDeclarationStatement(
                name: component.ComponentNameValidLowercaseFirst(),
                initializer: generator.CastExpression(
                    type: generator.IdentifierName(component.GetTypeName()),
                    expression: generator.IdentifierName("component")
                )
            ); // var typeOfComponent = (TypeOfComponent)component;

            var castedComponent = generator.IdentifierName(component.ComponentNameValidLowercaseFirst());

            if (component.ContainsKey(ShouldGenerateComponentComponentDataExtension.COMPONENT_OBJECT_TYPE))
            { // Else if this is a generated component created to hold a non-IComponent object...

                return new[]
                {
                    castedComponentDeclaration,
                    generator.InvocationExpression(
                        expression: generator.MemberAccessExpression(entity, $"Replace{component.ComponentName()}"),
                        arguments: generator.MemberAccessExpression(castedComponent, "value")
                    ),
                };
            }
            else if (!component.IsEvent() && component.ContainsKey(EventComponentDataExtension.COMPONENT_EVENT_DATA))
            { // Else if this is a generated listener component...
                return new[]
                {
                    castedComponentDeclaration,
                };
            }
            else
            { // Else if this is anything else...
                return new[]
                {
                    castedComponentDeclaration,
                    generator.InvocationExpression(
                        expression: generator.MemberAccessExpression(entity, $"Replace{component.ComponentName()}"),
                        arguments: component
                            .GetSymbol()
                            .GetMembers()
                            .OfType<IFieldSymbol>()
                            .Where(f => !f.IsStatic && !f.HasConstantValue)
                            .Select(f => CopyParameter(generator, castedComponent, component, f))
                    ),
                };
            }
        }

        // TODO: If the component itself is being generated, just copy the original component's value

        private SyntaxNode CopyParameter(
            SyntaxGenerator generator,
            SyntaxNode castedComponent,
            ComponentData component,
            IFieldSymbol field
        )
        {
            if (component == null)
            {
                throw new ArgumentNullException(nameof(component));
            }

            if (field == null)
            {
                throw new ArgumentNullException(nameof(field));
            }

            var sameField = generator.MemberAccessExpression(
                castedComponent,
                memberName: field.Name
            ); // ((ComponentType)component).field

            switch (field.Type)
            {
                case IArrayTypeSymbol array: // If this is an array...
                    return generator.CastExpression(
                        type: array,
                        expression: generator.InvocationExpression(
                            generator.MemberAccessExpression(sameField, nameof(ICloneable.Clone))
                        )
                    ); // (type[])this.ComponentName.fieldName.Clone()

                case IPointerTypeSymbol pointer: // Else if this is a pointer type...
                    return sameField;
                case INamedTypeSymbol type: // Else if this is basically any other type...
                {
                    var typeArgs = type.TypeArguments;
                    var compilation = (Compilation)objectCache[ExposeRoslynProject.CompilationKey];

                    // Using type names to figure out if this field is a built-in type because
                    // I can't get the info I need from Roslyn without access to the original
                    // Compilation object

                    switch (type)
                    {
                        case var _ when type.OriginalDefinition.SpecialType ==
                                        SpecialType.System_Collections_Generic_IList_T:
                        case var _ when type.IsGenericCollection("IList<T>"):
                        case var _ when type.IsGenericCollection("List<T>"):
                        case var _ when type.IsGenericCollection("Dictionary<TKey, TValue>"):
                        case var _ when type.IsGenericCollection("SortedDictionary<TKey, TValue>"):
                        case var _ when type.IsGenericCollection("IDictionary<TKey, TValue>"):
                        case var _ when type.IsGenericCollection("SortedSet<T>"):
                        case var _ when type.IsGenericCollection("ISet<T>"):
                        case var _ when type.IsGenericCollection("HashSet<T>"):
                        case var _ when type.IsGenericCollection("Queue<T>"):
                        case var _ when type.IsGenericCollection("Stack<T>"):
                            return generator.ObjectCreationExpression(type, sameField); // new List<T>(this.list)

                        // TODO: If this type has a one-argument constructor accepting the same type, call new TheType(other)
                        default:
                            // Includes:
                            //   - primitive types
                            //   - strings
                            //   - value types
                            //   - enums
                            //   - pointers
                            //   - delegates
                            //   - all reference types not handled by above cases
                            return sameField;
                    }

                    // TODO: If this is a read-only collection of read-only types, just return a reference
                    break;
                }
                default:
                    return sameField;
            }
        }
    }
}
