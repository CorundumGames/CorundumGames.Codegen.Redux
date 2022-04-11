using System;
using System.Collections.Generic;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CLI.Utils;
using Entitas.CodeGeneration.Plugins;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PostProcessors
{
    [PublicAPI]
    public sealed class LogCodeGeneratorData : ICodeGenerator
    {
        public string name => "LogCodeGeneratorDataPostProcessor";
        public int priority => int.MinValue;
        // Run after everything else

        public bool runInDryMode => true;

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            if (Environment.GetCommandLineArgs().IsDebug())
            { // If we're running the code generator in debug mode...
                foreach (var d in data)
                {
                    Print(d);
                }
            }

            return Array.Empty<CodeGenFile>();
        }

        private static void Print(CodeGeneratorData data)
        {
            // TODO: Handle the case where this is null
            Console.WriteLine(data.ToString());
            foreach (var d in data)
            {
                Console.Write($"\t{d.Key} ({d.Value.GetType()}):");
                switch (d.Value)
                {
                    case string[] { Length: > 0 } strings:
                        Console.WriteLine("");
                        Print("\t", strings);
                        break;
                    case MemberData memberData:
                        Console.WriteLine("");
                        Print("\t", memberData);
                        break;
                    case MemberData[] { Length: > 0 } memberData:
                        Console.WriteLine("");
                        Print("\t", memberData);
                        break;
                    case EventData eventData:
                        Console.WriteLine("");
                        Print("\t", eventData);
                        break;
                    case EventData[] { Length: > 0 } eventData:
                        Console.WriteLine("");
                        Print("\t", eventData);
                        break;

                    case Array { Length: 0 }:
                        Console.WriteLine(" <empty>");
                        break;
                    default:
                        Console.WriteLine($" {d.Value}");
                        break;
                }
            }
        }

        private static void Print(string indent, IEnumerable<string> strings)
        {
            foreach (var s in strings)
            {
                Console.WriteLine($"{indent}\t- {s}");
            }
        }

        private static void Print(string indent, MemberData data)
        {
            Console.WriteLine($"{indent}\t{data.name}: {data.type}");
        }

        private static void Print(string indent, IEnumerable<MemberData> data)
        {
            foreach (var d in data)
            {
                Print(indent, d);
            }
        }

        private static void Print(string indent, EventData data)
        {
            Console.WriteLine($"{indent}\t- {data.eventTarget}.{data.eventType} (priority {data.priority})");
        }


        private static void Print(string indent, IEnumerable<EventData> data)
        {
            foreach (var d in data)
            {
                Print(indent, d);
            }
        }

        // TODO: support Entitas.CodeGeneration.Plugins.MethodData[]
    }
}
