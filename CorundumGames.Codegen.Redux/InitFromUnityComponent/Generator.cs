using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CorundumGames.CodeGeneration.Plugins.Base;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.InitFromUnityComponent
{
    [PublicAPI]
    public sealed class Generator : ICodeGenerator
    {
        public string name => "InitFromUnityComponent System Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        private const string FeatureName = "UnityComponentInitializerFeature";

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var types = data
                .OfType<Data>()
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
                    .SelectMany(Generate)
                    .Concat(featureFile)
                    .ToArray();
            }
            else
            {
                return featureFile;
            }
        }

        private IEnumerable<CodeGenFile> Generate(Data data)
        {
            var componentName = data.Name.ToComponentName(true);

            return from context in data.Contexts
                let className = $"{context}Init{componentName}System"
                let template = new SystemTemplate(data, context)
                select new CodeGenFile(
                    Path.Combine(context, "Systems", $"{className}.cs"),
                    template.TransformText(),
                    GetType().FullName
                );
        }

        private IEnumerable<string> GenerateSystemNames(Data data)
        {
            var componentName = data.Name.ToComponentName(true);

            return data.Contexts.Select(context => $"{context}Init{componentName}System");
        }
    }
}
