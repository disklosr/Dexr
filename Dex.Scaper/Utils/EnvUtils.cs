using System;
using System.Diagnostics;

namespace Dex.Scaper.Utils
{
    public class EnvUtils
    {
        public static void OpenJsonFile(string fileName)
        {
            RunCommand(@"C:\Program Files (x86)\Microsoft VS Code\Code.exe", $"-r {fileName}");
        }

        private static void RunCommand(string command, string args)
        {
            Process process = new Process();
            process.StartInfo.FileName = command;
            process.StartInfo.Arguments = args;
            process.Start();
        }
    }
}