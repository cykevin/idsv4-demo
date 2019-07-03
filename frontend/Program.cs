using IdentityModel.Client;
using System;
using System.Net.Http;

namespace frontend
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

            // request token
            var tokenResponse = client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disc.TokenEndpoint,
                ClientId = "ls365-web",
                ClientSecret = "666666",
                Scope = "information"
            }).Result;

            if (tokenResponse.IsError)
            {
                Console.WriteLine(tokenResponse.Error);
            }
            Console.WriteLine(tokenResponse.Json);

            client.SetBearerToken(tokenResponse.AccessToken);
            var features = client.GetAsync("http://localhost:5080/feature/index").GetAwaiter().GetResult()
                .Content.ReadAsStringAsync().GetAwaiter().GetResult();

            Console.WriteLine(features);

            Console.ReadLine();
        }
    }
}
