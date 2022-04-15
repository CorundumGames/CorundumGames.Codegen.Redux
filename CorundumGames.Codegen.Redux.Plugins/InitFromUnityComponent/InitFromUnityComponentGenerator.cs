using CorundumGames.Codegen.Redux.Plugins.Base;
using CorundumGames.Codegen.Redux.Plugins.InitFromUnityComponent;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.InitFromUnityComponent;

[PublicAPI]
public sealed class InitFromUnityComponentGenerator : AbstractGenerator
{
    public override string Name => "InitFromUnityComponent System Generator";

    private const string FeatureName = "UnityComponentInitializerFeature";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var types = data
            .OfType<InitFromUnityComponentData>()
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

    private IEnumerable<CodeGenFile> Generate(InitFromUnityComponentData initFromUnityComponentData)
    {
        var componentName = initFromUnityComponentData.Name.ToComponentName();

        return from context in initFromUnityComponentData.Contexts
            let className = $"{context}Init{componentName}System"
            let template = new SystemTemplate(initFromUnityComponentData, context)
            select new CodeGenFile(
                Path.Combine(context, "Systems", $"{className}.cs"),
                template.TransformText(),
                GetType().FullName
            );
    }

    private IEnumerable<string> GenerateSystemNames(InitFromUnityComponentData initFromUnityComponentData)
    {
        var componentName = initFromUnityComponentData.Name.ToComponentName();

        return initFromUnityComponentData.Contexts.Select(context => $"{context}Init{componentName}System");
    }
}
