using System;
using RestSharp;

namespace EOSAchievements
{
    public class Achievements
    {


        static void Main(string[] args)
        {
            Utils.Log("Detecting Eligable Programs");
            Utils.CheckForEligibleProcess();
            Console.Clear();
            Utils.Log("Grabbing GetUserAuthToken");
            Auth.GetUserAuthToken();
            Achievements.unlockall();
            Console.Read();
        }
            public static void unlockall()
            {
            if(Auth.RocketLeague == true)
            {
                for (int i = 0; i <= 89; i++)
                {
                    var restclient = new RestClient($"https://api.epicgames.dev/stats/v1/{Auth.deploymentID}/achievements/{Auth.ProductUserID}/unlock");
                    restclient.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", $"Bearer {Auth.AuthToken}");
                    request.AddHeader("Content-Type", "application/json");
                    string body = @$"[""{i}""]";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = restclient.Execute(request);
                    if (i < 89)
                    {
                        Utils.Log("Unlocked: " + i + "\n");
                    }
                }
            }
            if(Auth.Kena == true)
            {
                foreach (string Achievement in Structs.KenaAchievements)
                {
                    var restclient = new RestClient($"https://api.epicgames.dev/stats/v1/{Auth.deploymentID}/achievements/{Auth.ProductUserID}/unlock");
                    restclient.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", $"Bearer {Auth.AuthToken}");
                    request.AddHeader("Content-Type", "application/json");
                    string body = @$"[""{Achievement}""]";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = restclient.Execute(request);

                    Utils.Log("Unlocked: " + Achievement + "\n");
                }
            }
        }



    }
    }
