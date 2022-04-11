using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Roslyn.CodeGeneration.Plugins;
using DesperateDevs.Serialization;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class ExposeRoslynProject : IPreProcessor, IConfigurable, ICachable
    {
        public const string ProjectKey = "DesperateDevs.Roslyn.CodeGeneration.Plugins.Project";
        public const string CompilationKey = "DesperateDevs.Roslyn.CodeGeneration.Plugins.Compilation";

        public string name => "Expose Roslyn Project";
        public int priority => -1000;
        public bool runInDryMode => true;


        public Dictionary<string, string> defaultProperties => _projectPathConfig.defaultProperties;
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
            var parser = PluginUtil.GetCachedProjectParser(objectCache, _projectPathConfig.projectPath);

            var type = parser.GetType();
            var projectMember = type.GetField("_project", BindingFlags.Instance | BindingFlags.NonPublic);

            Debug.Assert(projectMember != null);

            var project = projectMember.GetValue(parser) as Project;

            Debug.Assert(project != null);

            var compilation = project.GetCompilationAsync().Result;
            objectCache[ProjectKey] = project;
            objectCache[CompilationKey] = compilation;
        }
    }
}
