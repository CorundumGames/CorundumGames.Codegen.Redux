using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.FixedCleanup;

[PublicAPI]
public sealed class FixedCleanupFeatureGenerator : AbstractGenerator
{
    public override string Name => nameof(FixedCleanupFeatureGenerator);

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var codeGenFiles = new List<CodeGenFile>();
        var componentData = data
            .OfType<FixedCleanupComponentData>()
            .ToArray();

        var contextNames = componentData
            .SelectMany(x => x.Contexts)
            .Distinct()
            .ToArray();

        foreach (var name in contextNames)
        {
            var contextComponentData = componentData.Where(x => x.Contexts.Contains(name));
            codeGenFiles.Add(GenerateSystems(name, contextComponentData));
        }

        return codeGenFiles.ToArray();
    }

    private CodeGenFile GenerateSystems(string contextName, IEnumerable<FixedCleanupComponentData> data)
    {
        var absoluteFilePath = Path.Combine(contextName, "Features", $"{contextName}FixedCleanupFeature.cs");
        var fileContents = new FixedCleanupFeatureGeneratorTemplate(contextName, data).TransformText();

        return new CodeGenFile(absoluteFilePath, fileContents, nameof(FixedCleanupFeatureGenerator));
    }
}
