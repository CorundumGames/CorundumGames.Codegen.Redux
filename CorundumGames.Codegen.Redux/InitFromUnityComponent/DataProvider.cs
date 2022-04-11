using System;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Entitas.CodeGeneration.Attributes;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Roslyn;
using DesperateDevs.Serialization;
using CorundumGames.CodeGeneration.Attributes;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;
using PluginUtil = DesperateDevs.Roslyn.CodeGeneration.Plugins.PluginUtil;

namespace CorundumGames.CodeGeneration.Plugins.InitFromUnityComponent
{
    [PublicAPI]
    public sealed class DataProvider : IDataProvider, IConfigurable, ICachable
    {
        public string name => "InitFromUnityComponent Data Provider"; // set this to match your dataprovider name
        public int priority => 0; // can be left at 0
        public bool runInDryMode => true; // allows this to run in dry-run mode, optional

        public Dictionary<string, string> defaultProperties => _projectPathConfig.defaultProperties;
        public Dictionary<string, object> objectCache { get; set; }
        private readonly ProjectPathConfig _projectPathConfig = new ProjectPathConfig();

        public void Configure(Preferences preferences)
        {
            _projectPathConfig.Configure(preferences);
        }

        // Define here which attribute you want to look for and the data type of the
        // code gen data we will create
        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        public CodeGeneratorData[] GetData()
        {
            try
            {
                return PluginUtil
                    .GetCachedProjectParser(objectCache, _projectPathConfig.projectPath)
                    .GetTypes()
                    .Where(type => type.GetAttribute<InitFromUnityComponentAttribute>() != null)
                    .Where(type => type.GetAttributes<ContextAttribute>(true).Length >= 1)
                    .Where(type => type.GetMembers().OfType<IFieldSymbol>().Count() == 1)
                    .Select(type => new Data
                    {
                        Name = type.Name,
                        Member = GetData(type),
                        Contexts = GetContexts(type),
                    })
                    .ToArray();
            }
            catch (NullReferenceException e)
            {
                return Array.Empty<CodeGeneratorData>();
            }
        }

        // this will get field type names and field names for each field in the class
        // replacing IFieldSymbol with IPropertySymbol would get property names and types intstead
        private MemberData GetData(INamedTypeSymbol type)
        {
            return type.GetMembers()
                .OfType<IFieldSymbol>()
                .Select(field => new MemberData(field.Type.ToCompilableString(), field.Name))
                .Single();
        }

        private string[] GetContexts(INamedTypeSymbol type)
        {
            return type
                    .GetAttributes<ContextAttribute>(true)
                    .Select(a => a.AttributeClass.Name.Replace("Attribute", ""))
                    .ToArray()
                ;
        }
    }
}
