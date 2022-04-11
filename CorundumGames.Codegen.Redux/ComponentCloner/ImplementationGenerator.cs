using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using CorundumGames.CodeGeneration.Plugins.PreProcessors;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CorundumGames.CodeGeneration.Plugins.ComponentCloner
{
    [PublicAPI]
    public sealed class ImplementationGenerator : ICodeGenerator, ICachable
    {
        public string name => "Component Cloner Implementation Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            return data
                .OfType<ContextData>()
                .Select(GenerateClonerImplementation)
                .ToArray();
        }

        private CodeGenFile GenerateClonerImplementation(ContextData data)
        {
            var project = objectCache[ExposeRoslynProject.ProjectKey] as Project;

            Debug.Assert(project != null);
            var generator = SyntaxGenerator.GetGenerator(project);

            var contextName = data.GetContextName();
            var className = contextName.AddEntitySuffix();

            var @class = generator.ClassDeclaration(
                name: className,
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Partial,
                interfaceTypes: new[]
                {
                    generator.IdentifierName(typeof(IComponentCloner).FullName)
                },
                members: new[]
                {
                    generator.MethodDeclaration(
                        name: nameof(IComponentCloner.CloneComponent),
                        parameters: new[]
                        {
                            generator.ParameterDeclaration("index", generator.IdentifierName(typeof(int).FullName)),
                            generator.ParameterDeclaration("component",
                                generator.IdentifierName(typeof(IComponent).FullName)),
                        },
                        accessibility: Accessibility.Public,
                        statements: new[]
                        {
                            generator.InvocationExpression(
                                expression: generator.ElementAccessExpression(
                                    generator.MemberAccessExpression(
                                        generator.IdentifierName($"{contextName}ComponentCloners"),
                                        "cloners"
                                    ), // ${Context}ComponentCloners.cloners
                                    generator.IdentifierName("index")
                                ), // [index]
                                arguments: new[]
                                {
                                    generator.ThisExpression(),
                                    generator.IdentifierName("component"),
                                }), // (this, component);
                        }
                    ),
                }
            );

            var code = generator.CompilationUnit(@class);

            return new CodeGenFile(
                fileContent: code.NormalizeWhitespace().ToFullString(),
                fileName: Path.Combine(contextName, $"{contextName.AddEntitySuffix()}.cs"),
                generatorName: this.GetType().FullName
            );
        }
    }
}
