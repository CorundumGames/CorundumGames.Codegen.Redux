using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.FixedCleanup;

[PublicAPI]
public sealed class FixedCleanupRemoveComponentSystemGenerator : AbstractGenerator
{
    public override string Name => nameof(FixedCleanupRemoveComponentSystemGenerator);

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        return data
            .OfType<FixedCleanupComponentData>()
            .SelectMany(GenerateCleanupSystems)
            .ToArray();
    }

    private IEnumerable<CodeGenFile> GenerateCleanupSystems(FixedCleanupComponentData data)
    {
        return data
            .Contexts
            .Select(contextName => GenerateCleanupSystem(contextName, data));
    }

    private CodeGenFile GenerateCleanupSystem(string contextName, FixedCleanupComponentData data)
    {
        var componentName = data.ComponentSymbol.Name.ToComponentName();
        var systemName = $"FixedRemove{componentName}From{contextName}EntitiesSystem";
        var absoluteFilePath = Path.Combine(contextName, "Systems", $"{systemName}.cs");
        var fileContents = new FixedCleanupRemoveComponentSystemGeneratorTemplate(data, contextName, systemName);

        return new CodeGenFile(
            absoluteFilePath,
            fileContents.TransformText(),
            nameof(FixedCleanupRemoveComponentSystemGenerator)
        );
    }
}
