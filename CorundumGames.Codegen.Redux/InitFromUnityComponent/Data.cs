using DesperateDevs.CodeGeneration;
using Entitas.CodeGeneration.Plugins;

namespace CorundumGames.CodeGeneration.Plugins.InitFromUnityComponent
{
    internal sealed class Data : CodeGeneratorData
    {
        // string keys to access the base disctionary with
        private const string NameKey = "InitFromUnityComponent.Name";
        public const string UnityComponentTypeKey = "InitFromUnityComponent.UnityComponentType";
        private const string MemberKey = "InitFromUnityComponent.Member";
        private const string ContextsKey = "InitFromUnityComponent.Contexts";

        // wrapper propery to access the dictionary
        public string Name
        {
            get => (string)this[NameKey];
            set => this[NameKey] = value;
        }

        // wrapper properties to access the dictionary
        public MemberData Member
        {
            get => (MemberData)this[MemberKey];
            set => this[MemberKey] = value;
        }

        public string[] Contexts
        {
            get => (string[])this[ContextsKey];
            set => this[ContextsKey] = value;
        }
    }
}
