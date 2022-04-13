using System.Collections.Generic;
using System.IO;
using System.Linq;
using CorundumGames.Codegen.Redux.Base;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.DisposableComponent
{
    [PublicAPI]
    public sealed class DisposableComponentGenerator : AbstractGenerator
    {
        private const string FeatureName = "DisposeDataFeature";
        public override string Name => "Disposable Component System Generator";

        public override CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var types = data
                .OfType<DisposableComponentData>()
                .ToArray();

            var names = types
                .SelectMany(GenerateSystemNames)
                .ToArray();

            var featureFile = new CodeGenFile[]
            {
                new(
                    Path.Combine("Features", $"{FeatureName}.cs"),
                    new FeatureGeneratorTemplate(FeatureName, names).TransformText(),
                    GetType().FullName
                ),
            };

            if (types.Any())
            {
                return types
                    .SelectMany(GenerateSystems)
                    .Concat(featureFile)
                    .ToArray();
            }
            else
            {
                return featureFile;
            }


        }

        private IEnumerable<CodeGenFile> GenerateSystems(DisposableComponentData data)
        {
            return from contextName in data.Contexts
                let template = new SystemTemplate(data.Name, contextName)
                let fileName = Path.Combine(contextName, "Systems", $"{template.SystemName}.cs")
                select new CodeGenFile(
                    fileName,
                    template.TransformText(),
                    GetType().FullName
                );
        }



        private IEnumerable<string> GenerateSystemNames(DisposableComponentData data)
        {
            var componentName = data.Name.ToComponentName();

            return data.Contexts.Select(context => $"DisposeOf{context}{componentName.RemoveComponentSuffix()}System");
        }
    }
}
