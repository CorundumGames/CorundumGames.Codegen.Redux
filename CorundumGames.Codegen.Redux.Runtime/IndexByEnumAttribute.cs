using System;

namespace CorundumGames.Codegen.Redux.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
    public sealed class IndexByEnumAttribute : Attribute
    {
        private readonly Type _componentType;

        public IndexByEnumAttribute(Type componentType)
        {
            _componentType = componentType;
        }
    }
}
