using System.Collections.Generic;
using System.IO;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Logging;
using DesperateDevs.Serialization;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PostProcessors
{
    [PublicAPI]
    public sealed class RestoreMetaFiles : IPostProcessor, ICachable, IConfigurable
    {
        public string name => "Restore .meta Files";

        public int priority => 101;

        public bool runInDryMode => false;

        private readonly TargetDirectoryConfig _targetDirectoryConfig = new();

        public Dictionary<string, object> objectCache { get; set; }

        public void Configure(Preferences preferences)
        {
            this._targetDirectoryConfig.Configure(preferences);
        }

        public Dictionary<string, string> defaultProperties => _targetDirectoryConfig.defaultProperties;

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            if (objectCache[PreserveMetaFiles.MetaFileCache] is Dictionary<string, byte[]> metaFiles)
            {
                foreach (var f in metaFiles)
                {
                    var metaFilePath = $"{f.Key}.meta";
                    File.WriteAllBytes(metaFilePath, f.Value);
                    fabl.Debug($"Restored {metaFilePath}");
                }
            }

            return files;
        }
    }
}
