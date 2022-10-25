using System;
using System.Linq;
using CorundumGames.Codegen.Redux.Runtime;
using EntitasRedux.Core.Plugins;
using Genesis.Plugin;
using JCMG.EntitasRedux;
using Microsoft.CodeAnalysis;

namespace CorundumGames.Codegen.Redux.Plugins.FixedCleanup;

internal sealed class FixedCleanupComponentData : CodeGeneratorData
{
    private const string ModesKey = "FixedCleanupComponentData.Modes";
    private const string ComponentSymbolKey = "FixedCleanupComponentData.ComponentSymbol";
    private const string ContextsKey = "FixedCleanupComponentData.Contexts";
    private const string MemberDataKey = "FixedCleanupComponentData.MemberData";
    private const string FlagPrefixKey = "FixedCleanupComponentData.FlagPrefix";

    public CleanupMode[] Modes
    {
        get => (CleanupMode[])this[ModesKey];
        set => this[ModesKey] = value;
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

    public MemberData[] MemberData
    {
        get => (MemberData[])this[MemberDataKey];
        set => this[MemberDataKey] = value;
    }

    public string FlagPrefix
    {
        get => (string)this[FlagPrefixKey];
        set => this[FlagPrefixKey] = value;
    }

    public FixedCleanupComponentData(ICachedNamedTypeSymbol componentSymbol)
    {
        ComponentSymbol = componentSymbol.NamedTypeSymbol ?? throw new ArgumentNullException(nameof(componentSymbol));
        Contexts = componentSymbol
            .GetAttributes(nameof(ContextAttribute), true)
            .Where(a => a.AttributeClass != null)
            .Select(a => a.AttributeClass!.Name.RemoveAttributeSuffix())
            .ToArray();

        Modes = componentSymbol
            .GetAttributes(nameof(FixedCleanupAttribute))
            .Select(d => (CleanupMode)d.ConstructorArguments[0].Value)
            .ToArray();

        MemberData = componentSymbol.GetPublicMemberData().ToArray();

        var attr = componentSymbol.GetAttributes(nameof(FlagPrefixAttribute)).SingleOrDefault();
        FlagPrefix = attr == null ? "is" : attr.ConstructorArguments[0].Value?.ToString();
    }
}
