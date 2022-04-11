using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using CorundumGames.CodeGeneration.Attributes;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Roslyn;
using DesperateDevs.Roslyn.CodeGeneration.Plugins;
using DesperateDevs.Serialization;
using Entitas.CodeGeneration.Attributes;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration.Plugins.IndexByPlayer
{
    [PublicAPI]
    public sealed class DataProvider : IDataProvider, IConfigurable, ICachable
    {
        public string name => "Index By Player Data Provider";
        public int priority => 0;
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

        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        public CodeGeneratorData[] GetData()
        {
            try
            {
                return PluginUtil
                        .GetCachedProjectParser(objectCache, _projectPathConfig.projectPath)
                        .GetTypes()
                        .Where(type => type.GetAttribute<IndexByPlayerAttribute>() != null)
                        .Select(GenerateData)
                        .ToArray()
                    ;
            }
            catch (NullReferenceException e)
            {
                return Array.Empty<CodeGeneratorData>();
            }
        }

        private Data GenerateData(ITypeSymbol symbol)
        {
            return new Data
            {
                Symbol = symbol,
                Contexts = symbol
                    .GetAttributes<ContextAttribute>(true)
                    .Select(a => a.AttributeClass.Name.Replace("Attribute", ""))
                    .ToArray(),
            };
        }
    }
}
