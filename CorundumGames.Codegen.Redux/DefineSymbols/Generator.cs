using System.Linq;
using DesperateDevs.CodeGeneration;
using System.Collections.Generic;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;
using System.Diagnostics;
using CorundumGames.CodeGeneration.Plugins.PreProcessors;
using DesperateDevs.Utils;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace CorundumGames.CodeGeneration.Plugins.DefineSymbols
{
    [PublicAPI]
    public sealed class Generator : AbstractGenerator, ICacheable
    {
        private const string GeneratedClassName = "DefineSymbols";
        public   override string Name => "#define Symbols Generator";

        public void SetCache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
        }

        public void Configure(IGenesisConfig genesisConfig)
        {
            _assembliesConfig.Configure(genesisConfig);
        }

        public CodeGenFile[] Generate(CodeGeneratorData[] data)
        {
            var project = objectCache[ExposeRoslynProject.ProjectKey] as Project;

            Debug.Assert(project != null);
            var generator = SyntaxGenerator.GetGenerator(project);
            var symbolsData = data.OfType<Data>().Single();

            var @class = generator.ClassDeclaration(
                name: GeneratedClassName,
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Static,
                members: new[]
                {
                    CreateSymbolsProperty(generator),
                    CreateSymbolsField(symbolsData.Defines),
                }
            );
            // public static class DefineSymbols { ... }

            var code = generator.CompilationUnit(@class);

            return new[]
            {
                new CodeGenFile(
                    fileContent: code.NormalizeWhitespace().ToFullString(),
                    fileName: $"{GeneratedClassName}.cs",
                    generatorName: typeof(Generator).FullName
                ),
            };
        }

        private SyntaxNode CreateSymbolsField(string[] defines)
        {
            var stringType = PredefinedType(Token(SyntaxKind.StringKeyword));
            var arrayType = ArrayType(stringType)
                    .WithRankSpecifier(
                        ArrayRankSpecifier(
                            SingletonSeparatedList<ExpressionSyntax>(
                                OmittedArraySizeExpression()
                            )
                        )
                    )
                ;

            return FieldDeclaration( // private static readonly string[] _symbols
                        VariableDeclaration(arrayType)
                            .WithVariable(
                                VariableDeclarator("_symbols")
                                    .WithInitializer(
                                        EqualsValueClause( // =
                                            ArrayCreationExpression(arrayType) // new [] ...
                                                .WithInitializer(
                                                    InitializerExpression(SyntaxKind.ArrayInitializerExpression)
                                                        .WithCloseBraceToken(
                                                            Token(
                                                                TriviaList(defines.SelectMany(CreateDefine)),
                                                                SyntaxKind.CloseBraceToken,
                                                                TriviaList()
                                                            )
                                                        )
                                                )
                                        )
                                    )
                            )
                    )
                    .WithModifiers(SyntaxKind.PrivateKeyword, SyntaxKind.StaticKeyword, SyntaxKind.ReadOnlyKeyword)
                    .WithSemicolonToken()
                ;
        }

        private SyntaxTrivia[] CreateDefine(string define)
        {
            return new[]
            {
                Trivia(
                    IfDirectiveTrivia(
                        IdentifierName(define),
                        true,
                        false,
                        false
                    )
                ),
                DisabledText($"        \"{define}\",\n"),
                Trivia(EndIfDirectiveTrivia(true)),
            };
        }

        private SyntaxNode CreateSymbolsProperty(SyntaxGenerator generator)
        {
            return generator.PropertyDeclaration(
                name: "Symbols",
                accessibility: Accessibility.Public,
                modifiers: DeclarationModifiers.Static | DeclarationModifiers.ReadOnly,
                type: generator.IdentifierName(typeof(IReadOnlyList<string>).ToCompilableString()),
                getAccessorStatements: new[]
                {
                    generator.ReturnStatement(generator.IdentifierName("_symbols")),
                }
            );
        }
    }
}
