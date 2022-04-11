using DesperateDevs.CodeGeneration;
using Microsoft.CodeAnalysis;

namespace CorundumGames.CodeGeneration.Plugins.IndexByPlayer
{
    internal sealed class Data : CodeGeneratorData
    {
        // string keys to access the base dictionary with
        private const string SymbolKey = "IndexByPlayer.Symbol";
        private const string ContextsKey = "IndexByPlayer.Contexts";

        // wrapper property to access the dictionary
        public ITypeSymbol Symbol
        {
            get => (ITypeSymbol)this[SymbolKey];
            set => this[SymbolKey] = value;
        }

        public string[] Contexts
        {
            get => (string[]) this[ContextsKey];
            set => this[ContextsKey] = value;
        }
    }
}
