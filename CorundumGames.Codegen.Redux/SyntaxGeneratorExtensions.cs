using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Editing;

public static class SyntaxGeneratorExtensions
{
    public static SyntaxNode MemberAccessExpression(
        this SyntaxGenerator generator,
        string identifier,
        string memberName
    )
    {
        return generator.MemberAccessExpression(
            generator.IdentifierName(identifier),
            memberName
        );
    }

    public static SyntaxNode DefaultSwitchSection(
        this SyntaxGenerator generator,
        SyntaxNode statement
    )
    {
        return generator.DefaultSwitchSection(new[] {statement});
    }

    public static SyntaxNode SwitchSection(
        this SyntaxGenerator generator,
        SyntaxNode caseExpression,
        SyntaxNode statement
    )
    {
        return generator.SwitchSection(caseExpression, new[] {statement});
    }

    public static SyntaxNode TypeOfExpression<T>(this SyntaxGenerator generator)
    {
        return generator.TypeOfExpression(generator.IdentifierName(typeof(T).FullName));
    }
}
