using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CodeGeneration;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.IndexByPlayer
{
    [PublicAPI]
    public sealed class Generator : ICodeGenerator, ICachable
    {
        public string name => "Index by Player Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var indexByPlayerData = data
                .OfType<Data>()
                .ToArray();

            return new[]
            {
                new CodeGenFile(
                    fileContent: new ContextsExtensionsTemplate(indexByPlayerData).TransformText(),
                    fileName: "Contexts.cs",
                    generatorName: GetType().FullName
                ),
                new CodeGenFile(
                    fileContent: new ContextMethodTemplate(indexByPlayerData).TransformText(),
                    fileName: "Contexts.cs",
                    generatorName: GetType().FullName
                ),
            };
        }
    }
}
