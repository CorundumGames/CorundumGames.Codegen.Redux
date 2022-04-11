using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CLI.Utils;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Logging;
using DesperateDevs.Roslyn;
using DesperateDevs.Roslyn.CodeGeneration.Plugins;
using DesperateDevs.Serialization;
using Entitas;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class LogComponentAttributes : IPreProcessor, IConfigurable, ICachable
    {
        public string name => "Log components and their attributes";
        public int priority => int.MaxValue;
        // Run after everything else

        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => new();
        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }

        private readonly ProjectPathConfig _projectPathConfig = new();

        public void Configure(Preferences preferences)
        {
            _projectPathConfig.Configure(preferences);
        }


        public void PreProcess()
        {
            if (Environment.GetCommandLineArgs().IsDebug())
            { // If we're running the code generator in debug mode...
                var componentTypeName = typeof(IComponent).FullName;

                var types = PluginUtil
                    .GetCachedProjectParser(objectCache, _projectPathConfig.projectPath)
                    .GetTypes()
                    .Where(type => type.AllInterfaces.Any(i => i.ToCompilableString() == componentTypeName));

                foreach (var type in types)
                {

                    var attributes = type.GetAttributes();

                    if (attributes.Length == 0)
                    {
                        fabl.Debug($"{type.ToCompilableString()}: <none>");
                    }
                    else
                    {
                        fabl.Debug($"{type.ToCompilableString()}:");
                        foreach (var a in attributes)
                        {
                            fabl.Debug($"\t{a}");
                        }
                    }
                }
            }
        }
    }
}
