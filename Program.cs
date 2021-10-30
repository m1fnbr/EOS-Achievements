using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Collections.Generic;

namespace Claimer
{
    public class Achievements
    {

       public static bool RocketLeague;
       public static bool Kena;
        static void Main(string[] args)
        {
            Utils.Log("Pick An Option \n 1 For RocketLeague \n 2 For Kena");
            string Option = Console.ReadLine();
            if (Option == "1") { RocketLeague = true;} else if (Option == "2")Kena = true;
            Console.Clear();
            Utils.Log("Grabbing GetUserAuthToken");
            Auth.GetUserAuthToken();
            Achievements.unlockall();
            Console.Read();
        }
            public static void unlockall()
            {
            if(RocketLeague == true)
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
                    if (i < 89)
                    {
                        Utils.Log("Unlocked: " + i + "\n");
                    }
                }
            }
            if(Kena == true)
            {
                foreach (string Achievement in KenaAchievements)
                {
                    var client = new RestClient($"https://api.epicgames.dev/stats/v1/{Auth.deploymentID}/achievements/{Auth.ProductUserID}/unlock");
                    client.Timeout = -1;
                    var request = new RestRequest(Method.POST);
                    request.AddHeader("Authorization", $"Bearer {Auth.AuthToken}");
                    request.AddHeader("Content-Type", "application/json");
                    string body = @$"[""{Achievement}""]";
                    request.AddParameter("application/json", body, ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);

                    Utils.Log("Unlocked: " + Achievement + "\n");
                }
            }
        }



        static List<string> KenaAchievements = new List<string>(new string[]
{
            "ACH_PHOTO_MODE",
            "ACH_ABILITY_ALL",
            "ACH_MAIL_ALL" ,
            "ACH_CHALLENGE_ARROW" ,
            "ACH_CHESTS_ALL" ,
            "ACH_CHALLENGE_MAGE_BOMB" ,
            "ACH_CHALLENGE_MOTH" ,
            "ACH_CHALLENGE_MULTISHOT" ,
            "ACH_HERO_ROT" ,
            "ACH_CHALLENGE_SHIELD_STICKS" ,
            "ACH_CHALLENGE_COMBAT_ACTIONS" ,
            "ACH_LOCATION_FOREST" ,
            "ACH_HATS_ALL" ,
            "ACH_DIFFICULTY_MASTER" ,
            "ACH_ROT_ALL" ,
            "ACH_LOCATION_MOUNTAIN" ,
            "ACH_CHALLENGE_CRIT" ,
            "ACH_CHALLENGE_BOMB" ,
            "ACH_CHALLENGE_PARRY_KILL" ,
            "ACH_MEDITATION_ALL" ,
            "ACH_LOCATION_VILLAGE" ,
            "ACH_JIZO_ALL" ,
            "ACH_CHALLENGE_DASH_KILL" ,
            "ACH_JIZO_FIRST" ,
            "ACH_LOCATION_FARM"
});
    }
    }
