using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using IdentityServer4.Models;
//https://www.scottbrady91.com/Identity-Server/Getting-Started-with-IdentityServer-4
namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // identity resources like address,phone... etc
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope> 
            { 
                new ApiScope("WeatherForecastApi", "WeatherForecastApi API") 
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    
                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "WeatherForecastApi" }
                }
            };
        }
    }
}