using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.Context;

[PublicAPI]
public sealed class AttributeGenerator : AbstractGenerator
{
    public override string Name => "Context Attribute Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        return data
            .OfType<ContextData>()
            .Select(Generate)
            .ToArray();
    }

    private CodeGenFile Generate(ContextData data)
    {
        var contextName = data.GetContextName();
        return new CodeGenFile(
            Path.Combine("Attributes", $"{contextName}Attribute.cs"),
            new AttributeGeneratorTemplate(contextName).TransformText(),
            GetType().FullName
        );
    }
}
