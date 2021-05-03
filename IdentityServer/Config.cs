using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

using IdentityServer4.Models;
using IdentityServer4.Test;

namespace IdentityServer
{
    public class Config
    {
        /// <summary>
        /// Information about a user such as address,phone... etc
        /// </summary>
        /// <returns></returns>
        //public static IEnumerable<IdentityResource> GetIdentityResources()
        //{
        //    return new List<IdentityResource>
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        new IdentityResources.Email(),
        //        new IdentityResources.Phone(),
        //        new IdentityResources.Address(),
        //        new IdentityResource("roles", "User roles", new List<string> { "role" })
        //};
        //}

        //public static IEnumerable<ApiScope> GetApiScopes()
        //{
        //    return new List<ApiScope> 
        //    { 
        //        new ApiScope("WeatherForecastApi", "WeatherForecast Web API") 
        //    };
        //}

        ///// <summary>
        ///// Resources to protect
        ///// </summary>
        ///// <returns>List of resources to protect (ApiResource)</returns>
        //public static IEnumerable<ApiResource> GetApiResources()
        //{
        //    List<ApiResource> apiResources = new List<ApiResource>();
        //    ApiResource apiResource = new ApiResource("WeatherForecastApiResource", "WeatherForecast Web API")
        //    {
        //        Scopes = { "WeatherForecastApi" },
        //        UserClaims = { "role",
        //                    "given_name",
        //                    "family_name",
        //                    "email",
        //                    "phone",
        //                    "address"
        //                    }
        //    };

        //    apiResources.Add(apiResource);

        //    return apiResources;
        //}

        /// <summary>
        /// Clients to talk to the Web APIs
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                /*new Client
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
                }*/
                new Client
                {
                    ClientId = "spa_client",
                    ClientName = "SPA Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    ClientSecrets = new List<Secret>
                    {
                        new Secret("top_secret".Sha256())
                    },            
                    AllowedScopes = 
                    {
                        "openid",
                        "roles",
                        "WeatherForecastApi"
                     }
                }
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            TestUser usr1 = new TestUser()
            {
                SubjectId = "2f47f8f0-bea1-4f0e-ade1-88533a0eaf57",
                Username = "user1",
                Password = "password1",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "firstName1"),
                    new Claim("family_name", "lastName1"),
                    new Claim("address", "USA"),
                    new Claim("email","user1@localhost"),
                    new Claim("phone", "123"),
                    new Claim("role", "Admin")
                }
            };

            TestUser usr2 = new TestUser()
            {
                SubjectId = "5747df40-1bff-49ee-aadf-905bacb39a3a",
                Username = "user2",
                Password = "password2",
                Claims = new List<Claim>
                {
                    new Claim("given_name", "firstName2"),
                    new Claim("family_name", "lastName2"),
                    new Claim("address", "UK"),
                    new Claim("email","user2@localhost"),
                    new Claim("phone", "456"),
                    new Claim("role", "Operator")
                }
            };

            List<TestUser> testUsers = new List<TestUser>();
            testUsers.Add(usr1);
            testUsers.Add(usr2);

            return testUsers;
        }
        
    }
}