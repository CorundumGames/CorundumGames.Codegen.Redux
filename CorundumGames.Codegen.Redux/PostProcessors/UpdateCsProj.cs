using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Logging;
using DesperateDevs.Serialization;
using DesperateDevs.Utils;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PostProcessors
{
    [PublicAPI]
    public sealed class UpdateCsProj : IPostProcessor, IConfigurable
    {
        private const string Project = "Project";
        private const string ItemGroup = "ItemGroup";
        private const string Compile = "Compile";
        private const string Include = "Include";

        private readonly ProjectPathConfig _projectPathConfig = new();
        private readonly TargetDirectoryConfig _targetDirectoryConfig = new();
        private string _generatedScriptPrefix;

        public string name => "Update .csproj";

        public int priority => 96;

        public bool runInDryMode => false;

        public Dictionary<string, string> defaultProperties =>
            _projectPathConfig.defaultProperties
                .Merge(_targetDirectoryConfig.defaultProperties);

        public void Configure(Preferences preferences)
        {
            _projectPathConfig.Configure(preferences);
            _targetDirectoryConfig.Configure(preferences);
            _generatedScriptPrefix = _targetDirectoryConfig.targetDirectory.Replace('/', '\\');
        }

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            var document = new XmlDocument();
            // The XML isn't parsed with Microsoft.Build.Evaluation because that's not portable

            using (var reader = new XmlTextReader(_projectPathConfig.projectPath))
            {
                document.Load(reader);
            }

            var project = document[Project];
            if (project != null)
            {
                RemoveExistingGeneratedEntries(document);
                AddGeneratedEntries(document, files);

                var settings = new XmlWriterSettings
                {
                    Indent = true,
                    IndentChars = "    ",
                    NewLineHandling = NewLineHandling.Replace,
                    NewLineChars = "\n",
                    Encoding = Encoding.UTF8,
                    ConformanceLevel = ConformanceLevel.Document,
                };


                using var writer = XmlWriter.Create(_projectPathConfig.projectPath, settings);
                document.Save(writer);
            }
            else
            {
                fabl.Warn(
                    $"Couldn't find a top-level <Project> node in {_projectPathConfig.projectPath}. Not modifying it.");
            }

            return files;
        }

        private void RemoveExistingGeneratedEntries(XmlDocument document)
        {
            var generatedCompileNodes =
                document.SelectNodes($"Project/ItemGroup/Compile[starts-with(@Include, '{_generatedScriptPrefix}')]");

            if (generatedCompileNodes != null)
            {
                foreach (XmlNode n in generatedCompileNodes)
                {
                    n?.ParentNode?.RemoveChild(n);
                }
            }
        }

        private void AddGeneratedEntries(XmlDocument document, CodeGenFile[] files)
        {
            var project = document[Project];

            Debug.Assert(project != null);

            var itemGroup = project[ItemGroup];

            // Use the first <ItemGroup> found, or create a new one
            if (itemGroup == null)
            { // If this <Project> has no <ItemGroup>...
                itemGroup = document.CreateElement(ItemGroup);

                project.AppendChild(itemGroup);
            }

            foreach (var f in files.OrderBy(f => f.fileName))
            {
                var compile = document.CreateElement(Compile);
                var filename = f.fileName.Replace('/', '\\');
                compile.SetAttribute(Include, $@"{_generatedScriptPrefix}\{filename}");
                compile.RemoveAttribute("xmlns");

                itemGroup.AppendChild(compile);
            }
        }
    }
}
