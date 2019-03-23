using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ContactsManager.Client
{
    class Program {
        static void Main(string[] args) {
            temp().Wait();
        }
        public static async Task temp() {
            var identityServer = await DiscoveryClient.GetAsync("http://localhost:5000"); //discover the IdentityServer
            if (identityServer.IsError) {
                Console.Write(identityServer.Error);
                return;
            }

            HttpClient client = new HttpClient();

            var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest {
                Address = identityServer.TokenEndpoint,
                ClientId = "ClientId",
                ClientSecret = "secret",

                UserName = "user",
                Password = "P@ssw0rd@123",
                Scope = "ContactsManagerApiScope"
            });

            if (tokenResponse.IsError) {
                Console.WriteLine(tokenResponse.Error);
                return;
            }

            //Call the API

            client.SetBearerToken(tokenResponse.AccessToken);

            var response2 = await client.GetAsync("https://localhost:44396/api/values/1");

            var response = await client.GetAsync("https://localhost:44396/api/values");
            var content = await response.Content.ReadAsStringAsync();
            Console.WriteLine(JArray.Parse(content));
            Console.ReadKey();
        }
    }
}
