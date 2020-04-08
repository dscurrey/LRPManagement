using System.Collections.Generic;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace LRP.Auth
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
            return new ApiResource[]
            {
                new ApiResource("lrp_account_api", "LRP Account Management")
                {
                    UserClaims = { "name", "role" }
                },

                new ApiResource("lrp_web_api", "LRP Web Services")
                {
                    UserClaims = {"name", "role" }
                }
            };
        }

        public static IEnumerable<Client> GetIdentityClients(this IConfiguration configuration)
        {
            return new []
            {
                new Client
                {
                    ClientId = "lrp_web_api",
                    ClientName = "LARP API Service",
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
                    ClientId = "lrp_web_app",
                    ClientName = "LRP Web App",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        // Interact with auth server
                        "lrp_account_api",
                        // Use LRP API
                        "lrp_web_api",

                        // Sign in
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
