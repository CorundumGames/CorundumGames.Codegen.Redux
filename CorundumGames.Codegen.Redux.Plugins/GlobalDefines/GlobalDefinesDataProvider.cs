using Genesis.Plugin;
using Genesis.Shared;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.GlobalDefines;

[PublicAPI]
public sealed class GlobalDefinesDataProvider : IDataProvider, IConfigurable
{
    public string Name => "#define Symbols Data";
    public int Priority => 0;
    public bool RunInDryMode => true;

    private readonly GlobalDefinesConfig _config = new();

    public void Configure(IGenesisConfig genesisConfig)
    {
        _config.Configure(genesisConfig);
    }

    public CodeGeneratorData[] GetData()
    {
        return new CodeGeneratorData[]
        {
            new GlobalDefinesData
            {
                Defines = _config.Defines,
            },
        };
    }
}
