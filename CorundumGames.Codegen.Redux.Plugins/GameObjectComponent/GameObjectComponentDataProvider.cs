using System;
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
        var memberData = new MemberData("UnityEngine.GameObject", "value");
        var componentData = new ComponentData();
        componentData.SetMemberData(new[] { memberData });

        componentData.SetTypeName("GameObjectComponent");
        componentData.SetObjectTypeName("UnityEngine.GameObject");
        componentData.SetContextNames(_config.Contexts);
        componentData.ShouldGenerateMethods(true);
        componentData.ShouldGenerateComponent(true);
        componentData.IsUnique(false);
        componentData.IsEvent(false);
        componentData.ShouldGenerateIndex(true);
        componentData.SetFlagPrefix("Is");

        var indexData = new EntityIndexData();
        indexData.SetContextNames(componentData.GetContextNames());
        indexData.SetComponentType(componentData.GetTypeName());
        indexData.SetEntityIndexType("JCMG.EntitasRedux.PrimaryEntityIndex");
        indexData.SetHasMultiple(false);
        indexData.IsCustom(false);
        indexData.SetMemberName(memberData.name);
        indexData.SetEntityIndexName(componentData.GetTypeName().ToComponentName());
        indexData.SetKeyType(memberData.compilableTypeString);
        indexData.SetCustomMethods(Array.Empty<MethodData>());

        return new CodeGeneratorData[] { componentData, indexData };
    }
}
