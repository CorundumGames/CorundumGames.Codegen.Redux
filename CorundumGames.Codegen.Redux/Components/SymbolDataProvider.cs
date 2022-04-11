using Entitas.CodeGeneration.Plugins;
using Microsoft.CodeAnalysis;
using IComponentDataProvider = Entitas.Roslyn.CodeGeneration.Plugins.IComponentDataProvider;

namespace CorundumGames.CodeGeneration.Plugins.Components
{
    internal sealed class SymbolDataProvider : IComponentDataProvider
    {
        public void Provide(INamedTypeSymbol type, ComponentData data)
        {
            data.SetSymbol(type);
        }
    }
}
