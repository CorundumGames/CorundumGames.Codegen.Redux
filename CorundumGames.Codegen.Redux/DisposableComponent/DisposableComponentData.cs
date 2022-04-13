using Genesis.Plugin;

namespace CorundumGames.Codegen.Redux.DisposableComponent
{
    internal sealed class DisposableComponentData : CodeGeneratorData
    {
        // string keys to access the base dictionary with
        private const string NameKey = "DisposeData.Name";
        private const string ContextsKey = "DisposeData.Contexts";

        // wrapper property to access the dictionary
        public string Name
        {
            get => (string)this[NameKey];
            set => this[NameKey] = value;
        }
        
        public string[] Contexts
        {
            get => (string[])this[ContextsKey];
            set => this[ContextsKey] = value;
        }
    }
}
