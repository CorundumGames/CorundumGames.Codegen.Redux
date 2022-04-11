using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.EntityDefinition
{
    [PublicAPI]
    public sealed class ApplyEntityDefinitionGenerator : ICodeGenerator
    {
        public string name => "Apply Entity Definition Generator";
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


        private static CodeGenFile CreateFile(string context)
        {
            var template = new ApplyEntityDefinitionTemplate(context);

            return new CodeGenFile(
                Path.Combine(context, $"{context.AddEntitySuffix()}.cs"),
                template.TransformText(),
                typeof(ApplyEntityDefinitionGenerator).FullName
            );
        }
    }
}
