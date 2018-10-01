using System;
using System.Net.Http;
using System.Threading.Tasks;
using IdentityModel;
using IdentityModel.Client;

namespace ThirdPartyDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DiscoveryClient discoveryClient = new DiscoveryClient("http://localhost:5000");
            DiscoveryResponse discoveryResponse = await discoveryClient.GetAsync();
            TokenClient tokenClient = new TokenClient(discoveryResponse.TokenEndpoint,"client","secret");
            TokenResponse tokenResponse = await tokenClient.RequestClientCredentialsAsync("api");
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", tokenResponse.AccessToken);
            string response = await httpClient.GetStringAsync("http://localhost:5001/api/values");
            Console.WriteLine(response);
        }
    }
}
