using System;
using System.Threading.Tasks;
using IdentityModel.Client;

namespace PwdClient
{
    class Program
    {
        static async Task Main(string[] args)
        {
            DiscoveryClient discoveryClient = new DiscoveryClient("http://localhost:5000");
            DiscoveryResponse discoveryResponse = await discoveryClient.GetAsync();
            TokenClient tokenClient = new TokenClient(discoveryResponse.TokenEndpoint,"pwdClient","secret");
            TokenResponse tokenResponse = await tokenClient.RequestResourceOwnerPasswordAsync("jesse", "123456", "api");
            Console.WriteLine(tokenResponse.AccessToken);
        }
    }
}
