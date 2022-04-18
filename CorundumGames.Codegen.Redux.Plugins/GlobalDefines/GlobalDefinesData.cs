using Genesis.Plugin;

namespace CorundumGames.Codegen.Redux.Plugins.GlobalDefines;

internal sealed class GlobalDefinesData : CodeGeneratorData
{
    private const string DefinesKey = "CorundumGames.Codegen.Redux.GlobalDefinesData.Defines";

    public string[] Defines
    {
        get => (string[])this[DefinesKey];
        set => this[DefinesKey] = value;
    }
}
