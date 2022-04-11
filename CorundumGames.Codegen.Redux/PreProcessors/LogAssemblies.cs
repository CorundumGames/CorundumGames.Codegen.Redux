using System;
using System.Linq;
using DesperateDevs.CLI.Utils;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Logging;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class LogAssemblies : IPreProcessor
    {
        public string name => "Log loaded assemblies";
        public int priority => int.MinValue;
        public bool runInDryMode => true;

        public void PreProcess()
        {
            try
            {
                if (Environment.GetCommandLineArgs().IsDebug())
                { // If we're running the code generator in debug mode...
                    foreach (var a in AppDomain.CurrentDomain.GetAssemblies().OrderBy(a => a.ToString()))
                    {
                        fabl.Debug(a.IsDynamic ? $"{a} (<dynamic>)" : $"{a} ({a.Location})");
                    }
                }
            }
            catch (Exception e)
            {
                fabl.Error(e.StackTrace);
            }
        }
    }
}
