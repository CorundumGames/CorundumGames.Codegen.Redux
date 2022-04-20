using System;
using JCMG.EntitasRedux;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    [MeansImplicitUse]
    [PublicAPI]
    [BaseTypeRequired(typeof(IComponent))]
    public sealed class IndexByEnumAttribute : Attribute
    {
        private readonly Type _componentType;

        public IndexByEnumAttribute(Type componentType)
        {
            _componentType = componentType;
        }
    }
}
