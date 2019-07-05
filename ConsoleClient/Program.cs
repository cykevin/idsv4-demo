using IdentityModel.Client;
using System;
using System.Net.Http;
using static IdentityModel.OidcConstants;

namespace ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            var url = "http://localhost:5000";

            var client = new HttpClient();
            var disc = client.GetDiscoveryDocumentAsync(url).Result;
            if (disc.IsError)
            {
                Console.WriteLine(disc.Error);
            }
            else
            {
                // request token
                var tokenResponse = client.RequestPasswordTokenAsync(new PasswordTokenRequest
                {
                    Address = disc.TokenEndpoint,
                    ClientId = "client.ro",
                    ClientSecret = "666666",
                    Scope = "information openid profile",
                    
                    UserName="chenye",
                    Password="chenye"
                }).GetAwaiter().GetResult();

                if (tokenResponse.IsError)
                {
                    Console.WriteLine(tokenResponse.Error);
                }
                else
                {
                    Console.WriteLine(tokenResponse.Json);
                    client.SetBearerToken(tokenResponse.AccessToken);
                    var features = client.GetAsync("http://localhost:5080/feature/index").GetAwaiter().GetResult()
                        .Content.ReadAsStringAsync().GetAwaiter().GetResult();

                    Console.WriteLine(features);

                    client.SetBearerToken(tokenResponse.AccessToken);
                    var identityClaims = client.GetAsync(disc.UserInfoEndpoint).GetAwaiter().GetResult()
                        .Content.ReadAsStringAsync().GetAwaiter().GetResult();
                    Console.WriteLine(identityClaims);
                }

            }

            Console.ReadLine();
        }
    }
}
