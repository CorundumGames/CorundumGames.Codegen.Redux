using System;
using System.Collections.Generic;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.CodeGenerator;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class PreloadUnityAssemblies : IPreProcessor, IConfigurable, ICachable
    {
        public const string UnityDirEnvVar = "UNITY_DIR";

        public string name => "Preload Unity Assemblies";
        public int priority => -500;
        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => _assembliesConfig.defaultProperties;
        public Dictionary<string, object> objectCache { get; set; }

        private readonly AssembliesConfig _assembliesConfig = new();
        private readonly CodeGeneratorConfig _codeGeneratorConfig = new();

        public void Configure(Preferences preferences)
        {
            _assembliesConfig.Configure(preferences);
            _codeGeneratorConfig.Configure(preferences);
        }

        public void PreProcess()
        {
            var unityDir = Environment.GetEnvironmentVariable(UnityDirEnvVar);

            if (string.IsNullOrEmpty(unityDir))
            {
                return;
            }

            var paths = new List<string>();

            paths.AddRange(_codeGeneratorConfig.searchPaths);
            paths.Add(unityDir);

            AssemblyResolver resolver = null;
            try
            {
                resolver = AssemblyResolver.LoadAssemblies(false, paths.ToArray());
                foreach (var a in _assembliesConfig.assemblies)
                {
                    resolver.Load(a);
                }
            }
            finally
            {
                resolver?.Close();
            }
        }
    }
}
