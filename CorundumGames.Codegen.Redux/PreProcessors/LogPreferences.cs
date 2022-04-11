using System;
using System.Collections.Generic;
using DesperateDevs.CLI.Utils;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Logging;
using DesperateDevs.Serialization;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class LogPreferences : IPreProcessor, IConfigurable
    {
        public string name => "Console.WriteLine preferences";
        public int priority => int.MaxValue;
        // Run after everything else

        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => new();

        private Preferences _preferences;

        public void Configure(Preferences preferences)
        {
            _preferences = preferences;
        }

        public void PreProcess()
        {
            if (Environment.GetCommandLineArgs().IsDebug())
                // If we're running the code generator in debug mode...
            {
                foreach (var key in _preferences.keys)
                {
                    fabl.Debug($"{key}: {_preferences[key]}");
                }
            }
        }
    }
}
