using System;
using System.IO;
using System.Linq;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Plugins.EntityBehaviour;

[PublicAPI]
public sealed class EntityBehaviourGenerator : AbstractGenerator
{
    public override string Name => "Entity Behaviour Generator";

    public override CodeGenFile[] Generate(CodeGeneratorData[] data)
    {
        var gameObjectComponentData = data
            .OfType<ComponentData>()
            .FirstOrDefault(d => d.ComponentName() == "GameObject");

        if (gameObjectComponentData == null)
        {
            return Array.Empty<CodeGenFile>();
        }

        var contextNames = gameObjectComponentData.GetContextNames();

        return data
            .OfType<ContextData>()
            .Where(d => contextNames.Contains(d.GetContextName()))
            .Select(CreateFile)
            .ToArray();
    }

    private static CodeGenFile CreateFile(ContextData context)
    {
        var template = new EntityBehaviourTemplate(context);

        var contextName = context.GetContextName();

        return new CodeGenFile(
            Path.Combine(contextName, $"{contextName}EntityBehaviour.cs"),
            template.TransformText(),
            typeof(EntityBehaviourGenerator).FullName
        );
    }
}
