using System;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CLI.Utils;
using DesperateDevs.Logging;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class LogEnvironment : IPreProcessor
    {
        public string name => "Log environment";
        public int priority => 0;
        public bool runInDryMode => true;

        public void PreProcess()
        {
            if (Environment.GetCommandLineArgs().IsDebug())
            { // If we're running the code generator in debug mode...
                var vars = Environment.GetEnvironmentVariables();

                var keys = new object[vars.Count];
                vars.Keys.CopyTo(keys, 0);

                foreach (var key in keys.OrderBy(k => k.ToString()))
                {
                    fabl.Debug($"{key} = {vars[key]}");
                }
            }
        }
    }
}
