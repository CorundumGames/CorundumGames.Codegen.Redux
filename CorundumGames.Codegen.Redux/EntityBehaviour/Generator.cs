using System;
using System.IO;
using System.Linq;
using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.EntityBehaviour
{
    [PublicAPI]
    public sealed class Generator : ICodeGenerator
    {
        public string name => "Entity Behaviour Generator";
        public int priority => 0;
        public bool runInDryMode => true;

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
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
                typeof(Generator).FullName
            );
        }
    }
}
