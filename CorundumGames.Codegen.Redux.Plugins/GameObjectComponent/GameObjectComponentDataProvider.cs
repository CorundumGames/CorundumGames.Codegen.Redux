using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using Genesis.Shared;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.GameObjectComponent;

[PublicAPI]
public sealed class GameObjectComponentDataProvider : IDataProvider, IConfigurable
{
    public string Name => "GameObjectComponent Data Provider";
    public int Priority => 0;
    public bool RunInDryMode => true;

    private GameObjectComponentConfig _config = new();

    public void Configure(IGenesisConfig genesisConfig)
    {
        _config.Configure(genesisConfig);
    }

    public CodeGeneratorData[] GetData()
    {
        var componentData = new ComponentData();
        componentData.SetMemberData(new MemberData[]
        {
            new("UnityEngine.GameObject", "value"),
        });

        componentData.SetTypeName("GameObjectComponent");
        componentData.SetObjectTypeName("UnityEngine.GameObject");
        componentData.SetContextNames(_config.Contexts);
        componentData.ShouldGenerateMethods(true);
        componentData.ShouldGenerateComponent(true);
        componentData.IsUnique(false);
        componentData.IsEvent(false);
        componentData.ShouldGenerateIndex(true);
        componentData.SetFlagPrefix("Is");

        return new CodeGeneratorData[] { componentData };
    }
}
