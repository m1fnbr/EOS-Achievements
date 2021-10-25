using System;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using RestSharp;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading;
using System.Diagnostics;
using System.Net;

namespace Claimer
{
    public class Auth
    {

        public static string AuthToken;
        public static string ProductUserID;
        public static string deploymentID;
        public static string GetAuthCode()
        {
            Utils.Brave("https://www.epicgames.com/id/api/redirect?clientId=ec684b8c687f479fadea3cb2ad83f5c6&responseType=code");
            Utils.Log("Please Enter Auth Code: ");
            string authcode = Console.ReadLine();
            return authcode;
        }
        public static string ConvertAuthCodeToAccessToken()
        {
            var client = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "basic ZWM2ODRiOGM2ODdmNDc5ZmFkZWEzY2IyYWQ4M2Y1YzY6ZTFmMzFjMjExZjI4NDEzMTg2MjYyZDM3YTEzZmM4NGQ=");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "authorization_code");
            request.AddParameter("code", $"{GetAuthCode()}");
            IRestResponse response = client.Execute(request);
            string accesstoken = (string)JObject.Parse(response.Content)["access_token"];

            if(response.StatusCode == HttpStatusCode.BadRequest)
            {
                Utils.Log("Invalid Auth Code Please Try Again");
                Thread.Sleep(2000);
                Console.Clear();
                ConvertAuthCodeToAccessToken();

            }

            return accesstoken;
        }
        public static string GetExchangeCode()
        {
            var client = new RestClient("https://account-public-service-prod.ol.epicgames.com/account/api/oauth/exchange");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {ConvertAuthCodeToAccessToken()}");
            IRestResponse response = client.Execute(request);
            string exchangecode = (string)JObject.Parse(response.Content)["code"];
            return exchangecode;
        }
        public static string GetExternalAuthToken()
        {
            //Excuting the Request to get AuthToken
            var client = new RestClient("https://api.epicgames.dev/epic/oauth/v1/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Authorization", "Basic eHl6YTc4OTFwNUQ3czlSNkdtNm1vVEhXR2xvZXJwN0I6S25oMThkdTROVmxGcyszdVErWlBwRENWdG8wV1lmNHlYUDgrT2N3VnQxbw==");
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("grant_type", "exchange_code");
            request.AddParameter("deployment_id", "da32ae9c12ae40e8a112c52e1f17f3ba");
            request.AddParameter("exchange_code", $"{GetExchangeCode()}");
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
             AuthToken = (string)JObject.Parse(response.Content)["access_token"];
             ProductUserID = (string)JObject.Parse(response.Content)["product_user_id"];
             deploymentID = (string)JObject.Parse(response.Content)["deployment_id"];
        }
    }
}
