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
    public sealed class PreserveMetaFiles : IPostProcessor, ICachable, IConfigurable
    {
        public const string MetaFileCache = "CorundumGames.MetaFiles";

        public string name => "Preserve .meta Files";

        public int priority => -1;

        public bool runInDryMode => false;

        private readonly TargetDirectoryConfig _targetDirectoryConfig = new();

        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }


        public void Configure(Preferences preferences)
        {
            _targetDirectoryConfig.Configure(preferences);
        }

        public Dictionary<string, string> defaultProperties => _targetDirectoryConfig.defaultProperties;

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            var metaFileCache = new Dictionary<string, byte[]>(files.Length);

            foreach (var f in files)
            {
                var filePath = Path.Combine(_targetDirectoryConfig.targetDirectory, f.fileName);

                if (!metaFileCache.ContainsKey(filePath))
                { // Generated files with the same name are combined into one

                    var metaFilePath = $"{filePath}.meta";
                    metaFileCache[filePath] = File.ReadAllBytes(metaFilePath);
                    fabl.Debug($"Preserved {metaFilePath}");
                }
            }

            objectCache[MetaFileCache] = metaFileCache;

            return files;
        }
    }
}
