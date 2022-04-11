using System.Linq;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

public static class SyntaxExtensions
{
    public static PropertyDeclarationSyntax AddModifiers(
        this PropertyDeclarationSyntax syntax,
        params SyntaxKind[] modifiers
    )
    {
        return syntax.AddModifiers(modifiers.Select(Token).ToArray());
    }

    public static FieldDeclarationSyntax AddModifiers(
        this FieldDeclarationSyntax syntax,
        params SyntaxKind[] modifiers
    )
    {
        return syntax.AddModifiers(modifiers.Select(Token).ToArray());
    }


    public static AccessorDeclarationSyntax WithSemicolonToken(this AccessorDeclarationSyntax syntax)
    {
        return syntax.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    public static FieldDeclarationSyntax WithSemicolonToken(this FieldDeclarationSyntax syntax)
    {
        return syntax.WithSemicolonToken(Token(SyntaxKind.SemicolonToken));
    }

    public static ArrayTypeSyntax WithRankSpecifier(
        this ArrayTypeSyntax syntax,
        ArrayRankSpecifierSyntax rankSpecifier
    )
    {
        return syntax.WithRankSpecifiers(SingletonList(rankSpecifier));
    }

    public static FieldDeclarationSyntax WithModifiers(
        this FieldDeclarationSyntax syntax,
        params SyntaxKind[] modifiers
    )
    {
        return syntax.WithModifiers(
            TokenList(
                modifiers.Select(Token)
            )
        );
    }


    public static VariableDeclarationSyntax WithVariable(
        this VariableDeclarationSyntax syntax,
        VariableDeclaratorSyntax variable
    )
    {
        return syntax.WithVariables(SingletonSeparatedList(variable));
    }
}
