using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.Contexts
{
    [PublicAPI]
    public sealed class ContextObserverGenerator : ICodeGenerator
    {
        public string name => "ContextObserver Generator";
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
                    new ContextObserverTemplate(contextNames).TransformText(),
                    typeof(ContextObserverGenerator).FullName
                ),
            };
        }
    }
}
