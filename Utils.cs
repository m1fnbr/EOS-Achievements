using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;
using Colorful;
using System.Diagnostics;

namespace Claimer
{
    class Utils
    {
        public static void Log(string Log)
        {
            string Format = "{0} {1}";
            Formatter[] Formatting = new Formatter[]
            {
                new Formatter("[LOG] ", Color.Purple),
                new Formatter(Log,Color.Blue)

            };
            Console.WriteLineFormatted(Format,Color.White, Formatting);
        }

        public static void Brave(string URL)
        {
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            startInfo.FileName = "C:\\Program Files\\BraveSoftware\\Brave-Browser\\Application\\Brave.exe";
            startInfo.Arguments = $"https://www.epicgames.com/id/api/redirect?clientId=ec684b8c687f479fadea3cb2ad83f5c6&responseType=code";
            process.StartInfo = startInfo;
            process.Start();
        }
    }
}
