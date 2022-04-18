using Genesis.Shared;

namespace CorundumGames.Codegen.Redux.Plugins.GlobalDefines;

internal sealed class GlobalDefinesConfig : AbstractConfigurableConfig
{
    private const string DefinesKey = "CorundumGames.Codegen.Redux.Plugins.GlobalDefines.Defines";

    private static readonly string DefaultDefines = new[]
    {
        "DEBUG",
        "DEVELOPMENT_BUILD",
        "UNITY_64",
    }.ToCSV();

    public string[] Defines
    {
        get => _genesisConfig.GetOrSetValue(DefinesKey, DefaultDefines).ArrayFromCSV();
        set => _genesisConfig.SetValue(DefinesKey, value.ToCSV());
    }

    public override void Configure(IGenesisConfig genesisConfig)
    {
        base.Configure(genesisConfig);

        _genesisConfig.SetIfNotPresent(DefinesKey, DefaultDefines);
    }
}
