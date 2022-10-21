using JetBrains.Annotations;
using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;

namespace CorundumGames.Codegen.Redux.Plugins.Entity;

[PublicAPI]
public sealed class ComponentEntityApiInterfaceGenerator : AbstractGenerator
{
    public override string Name => "Component (Entity API Interface)";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        return data
            .OfType<ComponentData>()
            .Where(d => d.ShouldGenerateMethods())
            .Where(d => d.GetContextNames().Length > 1)
            .SelectMany(Generate)
            .ToArray();
    }

    private CodeGenFile[] Generate(ComponentData data)
    {
        return new[]
            {
                GenerateInterface(data)
            }
            .Concat(data.GetContextNames().Select(contextName => GenerateEntityInterface(contextName, data)))
            .ToArray();
    }

    private CodeGenFile GenerateInterface(ComponentData data)
    {
        var template = data.GetMemberData().Length == 0
            ? new ComponentEntityApiInterfaceGeneratorFlagTemplate(data).TransformText()
            : new ComponentEntityApiInterfaceGeneratorDataTemplate(data).TransformText();


        var fileName = Path.Join("Components", "Interfaces", $"I{data.ComponentName()}Entity.cs");

        return new CodeGenFile(fileName, template, nameof(ComponentEntityApiInterfaceGenerator));
    }

    private CodeGenFile GenerateEntityInterface(string contextName, ComponentData data)
    {
        var fileName = Path.Join(
            contextName,
            "Components",
            $"{data.ComponentNameWithContext(contextName).AddComponentSuffix()}.cs"
        );

        return new CodeGenFile(fileName,
            new ComponentEntityApiInterfaceGeneratorTemplate(data, contextName).TransformText(),
            nameof(ComponentEntityApiInterfaceGenerator)
        );
    }
}
