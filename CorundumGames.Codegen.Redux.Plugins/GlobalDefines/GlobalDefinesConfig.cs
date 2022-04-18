using Genesis.Shared;

namespace CorundumGames.Codegen.Redux.Plugins.GlobalDefines;

internal sealed class GlobalDefinesConfig : AbstractConfigurableConfig
{
    private const string DefaultDefinesKey = "CorundumGames.Codegen.Redux.Plugins.GlobalDefines.Defines";

    private static readonly string DefaultDefines = new[]
    {
        "DEBUG",
        "DEVELOPMENT_BUILD",
        "UNITY_64",
    }.ToCSV();

    public string[] Defines
    {
        get => _genesisConfig.GetOrSetValue(DefaultDefinesKey, DefaultDefines).ArrayFromCSV();
        set => _genesisConfig.SetValue(DefaultDefinesKey, value.ToCSV());
    }

    public override void Configure(IGenesisConfig genesisConfig)
    {
        base.Configure(genesisConfig);

        _genesisConfig.SetIfNotPresent(DefaultDefinesKey, DefaultDefines);
    }
}
