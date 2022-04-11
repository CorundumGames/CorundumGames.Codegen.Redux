using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.IO;
using CorundumGames.CodeGeneration.Plugins.PreProcessors;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

namespace CorundumGames.CodeGeneration.Plugins.Context
{
    [PublicAPI]
    public sealed class TypeEnumGenerator : ICodeGenerator, ICachable
    {
        private const string GeneratedClassName = "ContextType";

        public string name => "Context Enum Generator";
        public int priority => 0;
        public bool runInDryMode => true;
        public Dictionary<string, object> objectCache { get; set; }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var project = objectCache[ExposeRoslynProject.ProjectKey] as Project;

            Debug.Assert(project != null);
            var generator = SyntaxGenerator.GetGenerator(project);

            var @enum = generator.EnumDeclaration(
                name: GeneratedClassName,
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Static,
                members: data
                    .OfType<ContextData>()
                    .Select(d => generator.EnumMember(d.GetContextName()))
            );
            // public enum ContextType { ... }

            var code = generator.CompilationUnit(@enum);

            return new[]
            {
                new CodeGenFile(
                    fileContent: code.NormalizeWhitespace().ToFullString(),
                    fileName: Path.Combine("Enums", $"{GeneratedClassName}.cs"),
                    generatorName: typeof(TypeEnumGenerator).FullName
                )
            };

        }

    }
}
