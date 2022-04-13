using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using Genesis.Shared;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.GameObjectComponent
{
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
            var data = new ComponentData();
            data.SetMemberData(new MemberData[]
            {
                new("UnityEngine.GameObject", "gameObject"),
            });

            data.SetTypeName("GameObjectComponent");
            data.SetObjectTypeName("UnityEngine.GameObject");
            data.SetContextNames(_config.Contexts);
            data.ShouldGenerateMethods(true);
            data.ShouldGenerateComponent(true);
            data.IsUnique(false);
            data.IsEvent(false);
            data.ShouldGenerateIndex(true);
            data.SetFlagPrefix("Is");

            return new CodeGeneratorData[] { data };
        }
    }
}
