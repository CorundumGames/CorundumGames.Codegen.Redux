using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.Contexts
{
    [PublicAPI]
    public sealed class CreateEntityWithNameGenerator : ICodeGenerator
    {
        public string name => "Contexts.CreateEntity (string)";
        public int priority => 0;
        public bool runInDryMode => true;

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var contextNames = data
                .OfType<ContextData>()
                .Select(d => d.GetContextName())
                .OrderBy(contextName => contextName)
                .ToArray();

            return new[]
            {
                new CodeGenFile(
                    "Contexts.cs",
                    new CreateEntityWithNameTemplate(contextNames).TransformText(),
                    GetType().FullName
                ),
            };
        }
    }
}
