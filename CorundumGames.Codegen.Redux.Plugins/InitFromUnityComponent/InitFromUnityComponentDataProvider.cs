using System;
using System.Linq;
using CorundumGames.Codegen.Redux.Runtime;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using Genesis.Shared;
using JCMG.EntitasRedux;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace CorundumGames.Codegen.Redux.Plugins.InitFromUnityComponent;

[PublicAPI]
public sealed class InitFromUnityComponentDataProvider : IDataProvider, IConfigurable, ICacheable
{
    public string Name => "InitFromUnityComponent Data Provider";
    public int Priority => 0;
    public bool RunInDryMode => true;

    private readonly AssembliesConfig _assembliesConfig = new();
    private IMemoryCache? _memoryCache;

    public void SetCache(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
    }

    public void Configure(IGenesisConfig genesisConfig)
    {
        _assembliesConfig.Configure(genesisConfig);
    }

    public CodeGeneratorData[] GetData()
    {
        return _assembliesConfig
            .FilterTypeSymbols(_memoryCache.GetNamedTypeSymbols())
            .Where(type =>
                type.HasAttribute<InitFromUnityComponentAttribute>() &&
                type.HasAttribute<ContextAttribute>(true) &&
                type.AllPublicMembers.OfType<IFieldSymbol>().Count() == 1
            )
            .Select(type => new InitFromUnityComponentData
            {
                Name = type.TypeName,
                Member = GetData(type),
                Contexts = GetContexts(type),
            })
            .Cast<CodeGeneratorData>()
            .ToArray();
    }

    // this will get field type names and field names for each field in the class
    // replacing IFieldSymbol with IPropertySymbol would get property names and types intstead
    private MemberData GetData(ICachedNamedTypeSymbol type)
    {
        return type.AllPublicMembers
            .OfType<IFieldSymbol>()
            .Select(field => new MemberData(field, field.Type, field.Name))
            .Single();
    }

    private string[] GetContexts(ICachedNamedTypeSymbol type)
    {
        return type
            .GetAttributes(nameof(ContextAttribute), true)
            .Where(a => a.AttributeClass != null)
            .Select(a => a.AttributeClass!.Name.RemoveAttributeSuffix())
            .ToArray();
    }
}
