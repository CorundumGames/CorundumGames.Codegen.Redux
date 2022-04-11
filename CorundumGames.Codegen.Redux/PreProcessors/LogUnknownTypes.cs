using System;
using System.Collections.Generic;
using System.Linq;
using DesperateDevs.CLI.Utils;
using DesperateDevs.CodeGeneration;
using DesperateDevs.CodeGeneration.Plugins;
using DesperateDevs.Logging;
using DesperateDevs.Serialization;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace CorundumGames.CodeGeneration.Plugins.PreProcessors
{
    [PublicAPI]
    public sealed class LogUnknownTypes : IPreProcessor, IConfigurable, ICachable
    {
        private readonly Logger _logger = fabl.GetLogger(nameof(LogUnknownTypes));
        public string name => "Log Error Types";

        public int priority => int.MaxValue;
        // Run after everything else

        public bool runInDryMode => true;

        public Dictionary<string, string> defaultProperties => new();
        public Dictionary<string, object> objectCache
        {
            get;
            set;
        }

        private readonly ProjectPathConfig _projectPathConfig = new();

        public void Configure(Preferences preferences)
        {
            _projectPathConfig.Configure(preferences);
        }


        public void PreProcess()
        {
            if (Environment.GetCommandLineArgs().IsDebug())
            { // If we're running the code generator in debug mode...

                var unknownTypes = new List<IErrorTypeSymbol>();

                var compilation = (Compilation)objectCache[ExposeRoslynProject.CompilationKey];

                foreach (var tree in compilation.SyntaxTrees)
                { // For each parsed file...
                    // get the semantic model for this tree
                    var model = compilation.GetSemanticModel(tree);

                    // find everywhere in the AST that refers to a type
                    var root = tree.GetRoot();
                    var allTypeNames = root.DescendantNodesAndSelf().OfType<TypeSyntax>();

                    foreach (var typeName in allTypeNames)
                    {
                        // what does roslyn think the type _name_ actually refers to?
                        var effectiveType = model.GetTypeInfo(typeName);
                        if (effectiveType.Type is { TypeKind: TypeKind.Error } and IErrorTypeSymbol errorType)
                        {
                            // if it's an error type (ie. couldn't be resolved), cast and proceed
                            unknownTypes.Add(errorType);
                        }
                    }
                }

                foreach (var t in unknownTypes.Select(t => t.Name).OrderBy(t => t).Distinct())
                {
                    _logger.Warn($"Unknown type or namespace {t}");
                }
            }
        }
    }
}
