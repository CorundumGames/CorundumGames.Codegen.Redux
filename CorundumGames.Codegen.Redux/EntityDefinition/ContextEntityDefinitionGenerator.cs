using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.EntityDefinition
{
    [PublicAPI]
    public sealed class ContextEntityDefinitionGenerator : ICodeGenerator
    {
        public string name => "Context Entity Definition Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            return data
                .OfType<ContextData>()
                .Select(d => d.GetContextName())
                .Select(CreateFile)
                .ToArray();
        }


        private CodeGenFile CreateFile(string context)
        {
            var template = new ContextEntityDefinitionTemplate(context);

            return new CodeGenFile(
                Path.Combine(context, $"{context.AddEntitySuffix()}Definition.cs"),
                template.TransformText(),
                GetType().FullName
            );
        }
    }
}
