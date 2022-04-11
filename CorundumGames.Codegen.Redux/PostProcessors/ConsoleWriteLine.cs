using System;
using System.Linq;
using DesperateDevs.CodeGeneration;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PostProcessors
{
    [PublicAPI]
    public sealed class ConsoleWriteLine : IPostProcessor
    {
        public string name => "Console.WriteLine generated files";

        public int priority => 200;

        public bool runInDryMode => true;

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            var log = files
                .OrderBy(f => f.fileName)
                .Aggregate(
                    string.Empty, (acc, file) => $"{acc}{file.fileName} - {file.generatorName}\n"
                );

            Console.WriteLine(log);
            return files;
        }
    }
}
