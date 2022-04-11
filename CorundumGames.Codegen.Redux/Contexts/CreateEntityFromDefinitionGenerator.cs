using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.Contexts
{
    [PublicAPI]
    public sealed class CreateEntityFromDefinitionGenerator : ICodeGenerator
    {
        public string name => "Context.CreateEntity Generator (from EntityDefinition)";
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
            var template = new CreateEntityFromDefinitionTemplate(context);

            return new CodeGenFile(
                Path.Combine(context, $"{context.AddContextSuffix()}.cs"),
                template.TransformText(),
                typeof(CreateEntityFromDefinitionGenerator).FullName
            );
        }
    }
}
