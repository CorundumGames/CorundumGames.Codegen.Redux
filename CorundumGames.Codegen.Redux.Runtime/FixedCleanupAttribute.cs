using System;
using JCMG.EntitasRedux;
using JetBrains.Annotations;

namespace CorundumGames.Codegen.Redux.Runtime
{
    [PublicAPI]
    [AttributeUsage(
        AttributeTargets.Interface |
        AttributeTargets.Class |
        AttributeTargets.Struct |
        AttributeTargets.Enum,
        AllowMultiple = true)]
    public sealed class FixedCleanupAttribute : Attribute
    {
        public CleanupMode CleanupMode { get; }

        public FixedCleanupAttribute(CleanupMode cleanupMode)
        {
            CleanupMode = cleanupMode;
        }
    }
}
