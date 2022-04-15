using CorundumGames.Codegen.Redux.Runtime;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JCMG.EntitasRedux;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis;
using ISymbolExtensions = Genesis.Plugin.ISymbolExtensions;

namespace CorundumGames.Codegen.Redux.Plugins.IndexByEnum;

internal sealed class IndexByEnumData : CodeGeneratorData
{
    // string keys to access the base dictionary with
    private const string ComponentSymbolKey = "CorundumGames.Codegen.Redux.Plugins.IndexByEnum.ComponentSymbol";
    private const string EnumComponentSymbolKey = "CorundumGames.Codegen.Redux.Plugins.IndexByEnum.EnumComponentSymbol";
    private const string EnumMemberKey = "CorundumGames.Codegen.Redux.Plugins.IndexByEnum.EnumMember";
    private const string ContextsKey = "CorundumGames.Codegen.Redux.Plugins.IndexByEnum.Contexts";

    public MemberData EnumMember
    {
        get => (MemberData)this[EnumMemberKey];
        set => this[EnumMemberKey] = value;
    }

    public INamedTypeSymbol EnumComponentSymbol
    {
        get => (INamedTypeSymbol)this[EnumComponentSymbolKey];
        set => this[EnumComponentSymbolKey] = value;
    }

    public INamedTypeSymbol ComponentSymbol
    {
        get => (INamedTypeSymbol)this[ComponentSymbolKey];
        set => this[ComponentSymbolKey] = value;
    }

    public string[] Contexts
    {
        get => (string[])this[ContextsKey];
        set => this[ContextsKey] = value;
    }

    public IndexByEnumData(ICachedNamedTypeSymbol componentSymbol)
    {
        ComponentSymbol = componentSymbol.NamedTypeSymbol ?? throw new ArgumentNullException(nameof(componentSymbol));
        Contexts = componentSymbol
            .GetAttributes(nameof(ContextAttribute), true)
            .Where(a => a.AttributeClass != null)
            .Select(a => a.AttributeClass!.Name.RemoveAttributeSuffix())
            .ToArray();

        var indexByEnumAttribute = componentSymbol.GetAttributes(nameof(IndexByEnumAttribute)).Distinct().Single();
        var argument = indexByEnumAttribute.ConstructorArguments[0];
        if (argument.Kind == TypedConstantKind.Type && !argument.IsNull && argument.Value != null)
        {
            var enumComponentSymbol = (INamedTypeSymbol)argument.Value;
            EnumComponentSymbol = enumComponentSymbol;

            var enumMember = enumComponentSymbol
                .GetAllMembers()
                .OfType<IFieldSymbol>()
                .Single(s => s.DeclaredAccessibility == Accessibility.Public && s.Type.IsEnumType());

            EnumMember = new MemberData(enumMember, enumMember.Type, enumMember.Name);
        }
    }
}
