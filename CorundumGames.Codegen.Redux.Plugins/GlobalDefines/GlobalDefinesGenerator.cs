using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.GlobalDefines;

[PublicAPI]
public sealed class GlobalDefinesGenerator : AbstractGenerator
{
    private const string GeneratedClassName = "DefineSymbols";
    public override string Name => "#define Symbols Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var symbols = data
            .OfType<GlobalDefinesData>()
            .SelectMany(d => d.Defines)
            .ToArray();

        return new[]
        {
            new CodeGenFile(
                fileContent: new GlobalDefinesTemplate(symbols).TransformText(),
                fileName: $"{GeneratedClassName}.cs",
                generatorName: typeof(GlobalDefinesGenerator).FullName
            ),
        };
    }
}
