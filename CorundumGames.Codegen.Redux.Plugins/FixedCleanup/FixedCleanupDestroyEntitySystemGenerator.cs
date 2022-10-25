using System.Collections.Generic;
using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.FixedCleanup;

[PublicAPI]
public sealed class FixedCleanupDestroyEntitySystemGenerator : AbstractGenerator
{
    public override string Name => "Fixed Cleanup Destroy Entity System Generator";

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
        var fileName = $"FixedDestroy{contextName}EntitiesWith{componentName}System.cs";
        var absoluteFilePath = Path.Combine(contextName, "Systems", fileName);
        var fileContents = new FixedCleanupDestroyEntitySystemGeneratorTemplate(data, contextName).TransformText();

        return new CodeGenFile(absoluteFilePath, fileContents, nameof(FixedCleanupDestroyEntitySystemGenerator));
    }
}
