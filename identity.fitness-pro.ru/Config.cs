using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru
{
    public static class Config
    {
        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser
                {
                    SubjectId = "1",
                    Username = "alice",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Alice"),
                        new Claim("website", "https://alice.com")
                    }
                },
                new TestUser
                {
                    SubjectId = "2",
                    Username = "bob",
                    Password = "password",

                    Claims = new []
                    {
                        new Claim("name", "Bob"),
                        new Claim("website", "https://bob.com"),
                        new Claim("Role", "Student"),
                        new Claim("Dogovor", "Common"),
                        new Claim("Discount", "false")
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResource()
                {
                    DisplayName = "Role",
                    Name = "Role",
                    UserClaims = new List<string>()
                    {
                        "Role"
                    }
                },
                new IdentityResource()
                {
                    DisplayName = "Dogovor",
                    Name = "Dogovor",
                    UserClaims = new List<string>()
                    {
                        "Dogovor"
                    }
                },
                new IdentityResource()
                {
                    DisplayName = "Discount",
                    Name = "Discount",
                    UserClaims = new List<string>()
                    {
                        "Discount"
                    }
                }
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API",

                new[] { JwtClaimTypes.Name, JwtClaimTypes.Role, "office" }) // Claims to be included in access token)
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",

                    // no interactive user, use the clientid/secret for authentication
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    // secret for authentication
                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    // scopes that client has access to
                    AllowedScopes = { "api1" }
                },
                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = { "api1" }
                },
                // OpenID Connect hybrid flow client (MVC)
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets = GetSecret(),

                    RedirectUris           = { "http://localhost:5002/signin-oidc" },
                    PostLogoutRedirectUris = { "http://localhost:5002/signout-callback-oidc" },

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedScopes =  GetScopes(),

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RequireConsent = false
                }
            };
        }

        private static ICollection<Secret> GetSecret()
        {
            return new List<Secret> { new Secret("secret".Sha256()) };
        }

        private static ICollection<string> GetScopes()
        {
            return new List<string> {
                IdentityServerConstants.StandardScopes.OpenId,
                IdentityServerConstants.StandardScopes.Profile,
                "api1",
                "Role",
                "Discount"
            };
        }
    }
}
