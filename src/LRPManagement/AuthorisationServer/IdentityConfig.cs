using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace AuthorisationServer
{
    public static class IdentityConfigurationExtensions
    {
        public static IEnumerable<IdentityResource> GetIdentityResources(this IConfiguration configuration)
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),

                new IdentityResources.Profile(),

                new IdentityResource(name: "roles",
                                     displayName: "LRP Application Roles",
                                     claimTypes: new [] { "role" })
            };
        }

        public static IEnumerable<ApiResource> GetIdentityApis(this IConfiguration configuration)
        {
            return new[]
            {
                new ApiResource("lrp_account_api", "LRP Account Management"),

                new ApiResource("my_web_api", "Example web Service")
                {
                    UserClaims = { "name", "role" }
                }
            };
        }

        public static IEnumerable<Client> GetIdentityClients(this IConfiguration configuration)
        {
            return new[]
            {
                new Client
                {
                    ClientId = "my_web_api",
                    ClientName = "Example API Application",

                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        "lrp_account_api"
                    },

                    RequireConsent = false
                },

                new Client
                {
                    ClientId = "my_web_app",
                    ClientName = "Example Web App",

                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        "lrp_account_api",
                        "my_web_api",
                        "openid",
                        "profile",
                        "roles"
                    },

                    RequireConsent = false
                }
            };
        }
    }
}
