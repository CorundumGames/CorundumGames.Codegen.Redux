using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.Context;

[PublicAPI]
public sealed class DisposableContextGenerator : AbstractGenerator
{
    public override string Name => "Disposable Context Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        return data
            .OfType<ContextData>()
            .Select(d => d.GetContextName())
            .Select(CreateFile)
            .ToArray();
    }

    private CodeGenFile CreateFile(string context)
    {
        var template = new DisposableContextGeneratorTemplate(context);

        return new CodeGenFile(
            Path.Combine(context, $"{context.AddContextSuffix()}.cs"),
            template.TransformText(),
            typeof(DisposableContextGenerator).FullName
        );
    }
}
