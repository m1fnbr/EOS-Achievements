using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;

namespace Claimer
{
    public class Achievements
    {
        static void Main(string[] args)
        {
            Utils.Log("Grabbing GetUserAuthToken");
            Achievements.unlockall();
            Console.Read();
        }
            public static void unlockall()
            {
            for (int i = 0; i <= 89; i++)
            {
                var client = new RestClient($"https://api.epicgames.dev/stats/v1/{Auth.deploymentID}/achievements/{Auth.ProductUserID}/unlock");
                client.Timeout = -1;
                var request = new RestRequest(Method.POST);
                request.AddHeader("Authorization", $"Bearer {Auth.AuthToken}");
                request.AddHeader("Content-Type", "application/json");
                string body = @$"[""{i}""]";
                request.AddParameter("application/json", body, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

            Console.WriteLine("Unlocked: (" + i + "/" + "89) Achievements \n");
                if (i < 89)
                {
                    Console.Clear();
                }
            }
        }
        }
    }
