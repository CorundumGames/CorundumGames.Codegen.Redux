using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Genesis.Plugin;
using Genesis.Shared;
using JCMG.EntitasRedux;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using IComponent = System.ComponentModel.IComponent;

namespace CorundumGames.Codegen.Redux.DisposableComponent
{
    [PublicAPI]
    public sealed class DataProvider : IDataProvider, IConfigurable, ICacheable
    {
        private static readonly string DisposableName = typeof(IDisposable).FullName!;
        private static readonly string ComponentName = typeof(IComponent).FullName!;

        public string Name => "Disposable Component Data Provider";
        public int Priority => 0;
        public bool RunInDryMode => true;

        private readonly AssembliesConfig _assembliesConfig = new();
        private IMemoryCache _memoryCache;

        public void SetCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public void Configure(IGenesisConfig genesisConfig)
        {
            _assembliesConfig.Configure(genesisConfig);
        }

        [SuppressMessage("ReSharper", "CoVariantArrayConversion")]
        public CodeGeneratorData[] GetData()
        {
            return _assembliesConfig
                .FilterTypeSymbols(_memoryCache.GetNamedTypeSymbols())
                .Where(type=> type.ImplementsInterface<IComponent>() && type.ImplementsInterface<IDisposable>())
                .Select(type => new DisposableComponentData
                {
                    Name = type.TypeName,
                    Contexts = GetContexts(type),
                })
                .ToArray();
        }

        private static string[] GetContexts(ICachedNamedTypeSymbol type)
        {
            return type
                .GetAttributes("ContextAttribute", true)
                .Select(a => a.AttributeClass.Name.RemoveAttributeSuffix())
                .ToArray();
        }
    }
}
