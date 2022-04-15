using Genesis.Shared;

namespace CorundumGames.Codegen.Redux.Plugins.GameObjectComponent;

internal sealed class GameObjectComponentConfig : AbstractConfigurableConfig
{
    public const string ComponentContextsKey = "CorundumGames.Codegen.Redux.Plugins.GameObjectComponent.Contexts";
    private const string ComponentContextsDefault = "Game, Input";
    public string[] Contexts
    {
        get => _genesisConfig.GetOrSetValue(ComponentContextsKey, ComponentContextsDefault).ArrayFromCSV();
        set => _genesisConfig.SetValue(ComponentContextsKey, value.ToCSV());
    }


    public override void Configure(IGenesisConfig genesisConfig)
    {
        base.Configure(genesisConfig);

        _genesisConfig.SetIfNotPresent(ComponentContextsKey, ComponentContextsDefault);
    }
}
