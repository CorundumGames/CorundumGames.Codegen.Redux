using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.Contexts;

[PublicAPI]
public sealed class DisposableContextsGenerator : AbstractGenerator
{
    public override string Name => "Disposable ContextsW";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var contextNames = data
            .OfType<ContextData>()
            .Select(d => d.GetContextName())
            .OrderBy(contextName => contextName)
            .ToArray();

        return new[]
        {
            new CodeGenFile(
                "Contexts.cs",
                new DisposableContextsGeneratorTemplate(contextNames).TransformText(),
                typeof(DisposableContextsGenerator).FullName
            ),
        };
    }
}
