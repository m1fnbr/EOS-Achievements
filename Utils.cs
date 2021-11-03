using System.Drawing;
using Console = Colorful.Console;
using Colorful;
using System.Diagnostics;
using Microsoft.Win32;


namespace EOSAchievements
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
            Console.WriteLineFormatted(Format, Color.White, Formatting);
        }
        public static void Browse(string url)
        {
            string Browser = "iexplore.exe";
            RegistryKey UserChoiceAnswer = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\Shell\Associations\UrlAssociations\http\UserChoice");
            if (UserChoiceAnswer != null)
            {
                object progIdValue = UserChoiceAnswer.GetValue("Progid");
                if (progIdValue != null)
                {
                    if (progIdValue.ToString().ToLower().Contains("chrome"))
                        Browser = "chrome.exe";
                    else if (progIdValue.ToString().ToLower().Contains("firefox"))
                        Browser = "firefox.exe";
                    else if (progIdValue.ToString().ToLower().Contains("opera"))
                        Browser = "opera.exe";
                    else if (progIdValue.ToString().ToLower().Contains("brave"))
                        Browser = "brave.exe";
                }
                Process.Start(new ProcessStartInfo(Browser, url));
            }
        }
        public static void CheckForEligibleProcess()
        {
            // Forgor Kena Process Name lol, CBA to reinstall but it works lol
            Process[] RocketLeague = Process.GetProcessesByName("RocketLeague");
            if (RocketLeague.Length > 0) { Auth.RocketLeague = true; return; }
            else Utils.Log("No Eligible Processes Running, Do you mean to claim all achievements on Kena?"); string response = Console.ReadLine();

            if (response == "yes") { Console.Clear(); Auth.Kena = true; } else if (response == "no") ; Utils.Log("Restarting"); Console.Clear(); CheckForEligibleProcess();
        }
    }
}
