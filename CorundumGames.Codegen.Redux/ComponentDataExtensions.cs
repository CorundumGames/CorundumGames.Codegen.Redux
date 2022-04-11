using EntitasRedux.Core.Plugins;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration
{
    public static class ComponentDataExtensions
    {
        public const string COMPONENT_SYMBOL = "Component.Symbol";
        public const string COMPONENT_DISPOSABLE = "Component.Disposable";

        public static INamedTypeSymbol GetSymbol(this ComponentData data)
        {
            return (INamedTypeSymbol)data[COMPONENT_SYMBOL];
        }

        public static void SetSymbol(this ComponentData data, INamedTypeSymbol symbol)
        {
            data[COMPONENT_SYMBOL] = symbol;
        }

        public static bool IsDisposable(this ComponentData data)
        {
            return (bool)data[COMPONENT_DISPOSABLE];
        }

        public static void SetDisposable(this ComponentData data, bool disposable)
        {
            data[COMPONENT_DISPOSABLE] = disposable;
        }
    }
}
