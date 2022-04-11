using System.Text.RegularExpressions;
using DesperateDevs.CodeGeneration;
using JetBrains.Annotations;

namespace CorundumGames.CodeGeneration.Plugins.PostProcessors
{
    [PublicAPI]
    public sealed class NewLine : IPostProcessor
    {
        public string name => "Convert newlines";

        public int priority => 95;

        public bool runInDryMode => true;

        public CodeGenFile[] PostProcess(CodeGenFile[] files)
        {
            foreach (var file in files)
            {
                file.fileContent = Regex.Replace(file.fileContent, @"\r\n?|\n", "\r\n");
            }

            return files;
        }
    }
}
