using System;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis.CSharp;

public static class SymbolExtensions
{
    public static bool IsPartialClass(this INamedTypeSymbol type)
    {
        if (type.TypeKind is not (TypeKind.Class or TypeKind.Struct)) return false;

        return type
                .DeclaringSyntaxReferences
                .Select(s => s.GetSyntax())
                .SelectMany(s => s.ChildNodesAndTokens())
                .Any(n => n.IsKind(SyntaxKind.PartialKeyword))
            ;
    }

    public static bool IsGenericCollection(this INamedTypeSymbol type, string name)
    {
        return type.IsGenericType && type.OriginalDefinition.ToDisplayString() == $"System.Collections.Generic.{name}";
    }

    public static string FullName(this ITypeSymbol type)
    {
        if (type == null)
        {
            throw new ArgumentNullException(nameof(type));
        }

        if (type.IsTupleType)
        {
            return type.ToString();
        }


        var symbols = new List<INamespaceOrTypeSymbol> { type };


        for (
            var n = (INamespaceOrTypeSymbol)type.ContainingType ?? type.ContainingNamespace;
            n != null;
            n = (INamespaceOrTypeSymbol)n.ContainingType ?? n.ContainingNamespace
        )
        {
            symbols.Add(n);
        }


        return string.Join(
            ".",
            symbols
                .Select(n => n.Name)
                .Where(n => n.Length > 0)
                .Reverse()
        );
    }


}
