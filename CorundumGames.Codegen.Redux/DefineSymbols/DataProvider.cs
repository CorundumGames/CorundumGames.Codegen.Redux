using System.Collections.Generic;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.DefineSymbols
{
    [PublicAPI]
    public sealed class DataProvider : IDataProvider, IConfigurable, ICachable
    {
        public string name => "#define Symbols Data";
        public int priority => 0;
        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => _config.defaultProperties;
        public Dictionary<string, object> objectCache { get; set; }
        private readonly Config _config = new();

        public void Configure(Preferences preferences)
        {
            _config.Configure(preferences);
        }

        public CodeGeneratorData[] GetData()
        {
            return new CodeGeneratorData[]
            {
                new Data
                {
                    Defines = _config.Defines,
                },
            };
        }
    }
}
