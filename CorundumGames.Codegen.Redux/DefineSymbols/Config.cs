using DesperateDevs.Serialization;
using System.Collections.Generic;
using DesperateDevs.Utils;
using Genesis.Shared;

namespace CorundumGames.CodeGeneration.Plugins.DefineSymbols
{
    internal sealed class Config : AbstractConfigurableConfig
    {
        private const string ProjectPathKey = "CorundumGames.CodeGeneration.Plugins.DefineSymbols.Defines";

        public override Dictionary<string, string> defaultProperties => new()
        {
            [ProjectPathKey] = "DEBUG",
        };

        public string[] Defines
        {
            get => _preferences[ProjectPathKey].ArrayFromCSV();
            set => _preferences[ProjectPathKey] = value.ToCSV();
        }
    }
}
