using Genesis.Plugin;

namespace CorundumGames.CodeGeneration.Plugins.DefineSymbols
{
    internal sealed class Data : CodeGeneratorData
    {
        // string keys to access the base dictionary with
        private const string DefinesKey = "DefineSymbolsData.Defines";

        // wrapper property to access the dictionary
        public string[] Defines
        {
            get => (string[])this[DefinesKey];
            set => this[DefinesKey] = value;
        }
    }
}
