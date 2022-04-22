using System;
using System.Linq;
using CorundumGames.Codegen.Redux.Runtime;
using Genesis.Plugin;
using Genesis.Shared;
using JCMG.EntitasRedux;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.IndexByEnum;

[PublicAPI]
public sealed class IndexByEnumDataProvider : IDataProvider, IConfigurable, ICacheable
{
    public string Name => "Index By Enum Data Provider";
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
            .Where(type => type.ImplementsInterface<IComponent>() && type.HasAttribute<IndexByEnumAttribute>())
            .Select(type => new IndexByEnumData(type))
            .Cast<CodeGeneratorData>()
            .ToArray();
    }
}
