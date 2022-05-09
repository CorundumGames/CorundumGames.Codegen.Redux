using System;
using JCMG.EntitasRedux;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Runtime
{
    [AttributeUsage(AttributeTargets.Class)]
    [BaseTypeRequired(typeof(IComponent))]
    [MeansImplicitUse]
    [PublicAPI]
    public sealed class InitFromUnityComponentAttribute : Attribute
    {
    }
}
