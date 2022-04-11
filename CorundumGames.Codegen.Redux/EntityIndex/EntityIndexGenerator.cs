using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CodeGeneration;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.EntityIndex
{
    [PublicAPI]
    public sealed class EntityIndexGenerator : ICodeGenerator, IConfigurable
    {
        public string name => "Entity Index";

        public int priority => 0;

        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => _ignoreNamespacesConfig.defaultProperties;

        private readonly IgnoreNamespacesConfig _ignoreNamespacesConfig = new();

        public void Configure(Preferences preferences)
        {
            _ignoreNamespacesConfig.Configure(preferences);
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var entityIndexData = data
                .OfType<EntityIndexData>()
                .OrderBy(d => d.GetEntityIndexName())
                .ToArray();

            return entityIndexData.Length == 0
                ? Array.Empty<CodeGenFile>()
                : new[]
                {
                    new CodeGenFile(
                        "Contexts.cs",
                        new EntityIndexGeneratorTemplate(entityIndexData, _ignoreNamespacesConfig).TransformText(),
                        GetType().FullName
                    ),
                };

        }
    }
}
