using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;

namespace Claimer
{
    public class Auth
    {
        static string GetExternalAuthToken()
        {
            //Enter Exchange Code
            Console.WriteLine("Enter Exchange Code: ");
            string exchangecode = Console.ReadLine();

            //Excuting the Request to get AuthToken
            var client = new RestClient("https://api.epicgames.dev/epic/oauth/v1/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic eHl6YTc4OTFwNUQ3czlSNkdtNm1vVEhXR2xvZXJwN0I6S25oMThkdTROVmxGcyszdVErWlBwRENWdG8wV1lmNHlYUDgrT2N3VnQxbw==");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "exchange_code");
            request.AddParameter("deployment_id", "da32ae9c12ae40e8a112c52e1f17f3ba");
            request.AddParameter("exchange_code", $"{exchangecode}");
            request.AddParameter("scope", "basic_profile friends_list presence friends_management");
            IRestResponse response = client.Execute(request);


            //parsing response to get the token
            string ExternalAuthToken = (string)JObject.Parse(response.Content)["access_token"];
            return ExternalAuthToken;
        }


        public static void GetUserAuthToken()
        {

            //executing the request
            var client = new RestClient("https://api.epicgames.dev/auth/v1/oauth/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddHeader("Authorization", "Basic eHl6YTc4OTFwNUQ3czlSNkdtNm1vVEhXR2xvZXJwN0I6S25oMThkdTROVmxGcyszdVErWlBwRENWdG8wV1lmNHlYUDgrT2N3VnQxbw==");
            request.AddParameter("deployment_id", "da32ae9c12ae40e8a112c52e1f17f3ba");
            request.AddParameter("nonce", "2W5vgU-FxESeNw2tADe2Zg");
            request.AddParameter("external_auth_type", "epicgames_access_token");
            request.AddParameter("grant_type", "external_auth");
            request.AddParameter("external_auth_token", $"{GetExternalAuthToken()}");
            IRestResponse response = client.Execute(request);


            //parsing the response
            string AuthToken = (string)JObject.Parse(response.Content)["access_token"];
            string ProductUserID = (string)JObject.Parse(response.Content)["product_user_id"];
            string deploymentID = (string)JObject.Parse(response.Content)["deployment_id"];

            Achievements.unlockall(AuthToken, ProductUserID, deploymentID);
        }
    }
}
