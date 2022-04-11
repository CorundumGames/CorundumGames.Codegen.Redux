using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.Context
{
    [PublicAPI]
    public sealed class AttributeGenerator : ICodeGenerator
    {

        public string name => "Context Attribute Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        private const string Template =
            @"[JetBrains.Annotations.MeansImplicitUse(JetBrains.Annotations.ImplicitUseTargetFlags.WithMembers)]
public sealed class ${ContextName}Attribute : Entitas.CodeGeneration.Attributes.ContextAttribute {
    public ${ContextName}Attribute() : base(""${ContextName}"") {
    }
}
";

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            return data
                .OfType<ContextData>()
                .Select(Generate)
                .ToArray();
        }

        private CodeGenFile Generate(ContextData data)
        {
            var contextName = data.GetContextName();
            return new CodeGenFile(
                Path.Combine("Attributes", $"{contextName}Attribute.cs"),
                Template.Replace(contextName),
                GetType().FullName
            );
        }
    }
}
