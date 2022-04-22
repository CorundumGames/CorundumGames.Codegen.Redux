using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.Context;

[PublicAPI]
public sealed class TypeEnumGenerator : AbstractGenerator
{
    private const string GeneratedClassName = "ContextType";

    public override string Name => "Context Enum Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var contexts = data
            .OfType<ContextData>()
            .Select(d => d.GetContextName());

        return new[]
        {
            new CodeGenFile(
                fileContent: new TypeEnumGeneratorTemplate(contexts).TransformText(),
                fileName: Path.Combine("Enums", $"{GeneratedClassName}.cs"),
                generatorName: typeof(TypeEnumGenerator).FullName
            )
        };
    }
}
