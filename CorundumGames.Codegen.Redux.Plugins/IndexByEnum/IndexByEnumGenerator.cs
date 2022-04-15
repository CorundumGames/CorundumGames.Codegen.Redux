using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.IndexByEnum;

[PublicAPI]
public sealed class IndexByEnumGenerator : AbstractGenerator
{
    private const string FileName = "Contexts.cs";
    public override string Name => "Index by Player Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var indexByEnumData = data
            .OfType<IndexByEnumData>()
            .ToArray();

        var typeName = GetType().FullName;

        return new[]
        {
            new CodeGenFile(
                fileContent: new ContextsExtensionsTemplate(indexByEnumData).TransformText(),
                fileName: FileName,
                generatorName: typeName
            ),
            new CodeGenFile(
                fileContent: new ContextMethodTemplate(indexByEnumData).TransformText(),
                fileName: FileName,
                generatorName: typeName
            ),
        };
    }
}
