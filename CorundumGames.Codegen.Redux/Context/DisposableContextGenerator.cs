using System.Collections.Generic;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.Context
{
    [PublicAPI]
    public sealed class DisposableContextGenerator : ICodeGenerator, ICachable
    {
        public string name => "Disposable Context Generator";
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
                    .Select(d => d.GetContextName())
                    .Select(CreateFile)
                    .ToArray()
                ;
        }

        private CodeGenFile CreateFile(string context)
        {
            var template = new DisposableContextGeneratorTemplate(context);

            return new CodeGenFile(
                Path.Combine(context, $"{context.AddContextSuffix()}.cs"),
                template.TransformText(),
                typeof(DisposableContextGenerator).FullName
            );
        }
    }
}
