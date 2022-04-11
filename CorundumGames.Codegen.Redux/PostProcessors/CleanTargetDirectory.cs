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
    public sealed class CleanTargetDirectory : IPostProcessor, IConfigurable
    {
        private readonly Logger _logger = fabl.GetLogger(typeof(CleanTargetDirectory));
        private readonly TargetDirectoryConfig _targetDirectoryConfig = new();

        public string name => "Clean target directory";

        public int priority => 0;

        public bool runInDryMode => false;

        public Dictionary<string, string> defaultProperties => _targetDirectoryConfig.defaultProperties;

        public void Configure(Preferences preferences)
        {
            _targetDirectoryConfig.Configure(preferences);
        }

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            if (Directory.Exists(_targetDirectoryConfig.targetDirectory))
            {
                var directory = new DirectoryInfo(_targetDirectoryConfig.targetDirectory);
                foreach (var file in directory.GetFiles("*.cs", SearchOption.AllDirectories))
                {
                    try
                    {
                        File.Delete(file.FullName);
                    }
                    catch
                    {
                        _logger.Error($"Could not delete file {file.FullName}");
                    }
                }
            }
            else
            {
                Directory.CreateDirectory(_targetDirectoryConfig.targetDirectory);
            }

            return files;
        }
    }
}
