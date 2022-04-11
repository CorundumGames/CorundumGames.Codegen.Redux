using Entitas.Roslyn.CodeGeneration.Plugins;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration.Plugins.Components
{
    [PublicAPI]
    public sealed class ComponentDataProvider : Entitas.Roslyn.CodeGeneration.Plugins.ComponentDataProvider
    {
        public ComponentDataProvider() : this(null)
        {
        }

        public ComponentDataProvider(INamedTypeSymbol[] types)
            : base(types, GetComponentDataProviders())
        {
        }

        private static IComponentDataProvider[] GetComponentDataProviders()
        {
            return new IComponentDataProvider[]
            {
                new ComponentTypeComponentDataProvider(),
                new MemberDataComponentDataProvider(),
                new ContextsComponentDataProvider(),
                new IsUniqueComponentDataProvider(),
                new FlagPrefixComponentDataProvider(),
                new ShouldGenerateComponentComponentDataProvider(),
                new ShouldGenerateMethodsComponentDataProvider(),
                new ShouldGenerateComponentIndexComponentDataProvider(),
                new EventComponentDataProvider(),
                new SymbolDataProvider(),
            };
        }
    }
}
