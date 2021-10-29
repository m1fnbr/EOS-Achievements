using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Console = Colorful.Console;
using Colorful;
using System.Diagnostics;
using Microsoft.Win32;

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
    public static void Browse(string url)
    {
        string browserName = "iexplore.exe";
        using (RegistryKey UserChoiceAnswer = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice"))
        {
            if (UserChoiceAnswer != null)
            {
                object progIdValue = UserChoiceAnswer.GetValue("Progid");
                if (progIdValue != null)
                {
                    if(progIdValue.ToString().ToLower().Contains("chrome"))
                        browserName = "chrome.exe";
                    else if(progIdValue.ToString().ToLower().Contains("firefox"))
                        browserName = "firefox.exe";
                    else if (progIdValue.ToString().ToLower().Contains("safari"))
                        browserName = "safari.exe";
                    else if (progIdValue.ToString().ToLower().Contains("opera"))
                        browserName = "opera.exe";
                    else if (progIdValue.ToString().ToLower().Contains("brave"))
                             browserName = "brave.exe";
                }
            }
        }

        Process.Start(new ProcessStartInfo(browserName, url));
        }
    }
}
