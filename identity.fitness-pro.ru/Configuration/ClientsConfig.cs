using identity.fitness_pro.ru.Configuration.Models;
using IdentityServer4.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public static class ClientsConfig
    {
        public static IEnumerable<Client> GetClients(ClientSettingModel option)
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = option.ClientCredential_Anna.ClientId,
                    ClientName = option.ClientCredential_Anna.ClientName,
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(option.ClientCredential_Anna.Secret.Sha256())
                    },
                    AllowedScopes = option.ClientCredential_Anna.Scopes
                },
                new Client
                {
                    ClientId = option.ResourceOwner_Anabel.ClientId,
                    ClientName = option.ResourceOwner_Anabel.ClientName,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret(option.ResourceOwner_Anabel.Secret.Sha256())
                    },
                    AllowedScopes = option.ResourceOwner_Anabel.Scopes,
                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true
                },
                new Client
                {
                    ClientId = option.Hybrid_Alexa.ClientId,
                    ClientName = option.Hybrid_Alexa.ClientName,
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret(option.Hybrid_Alexa.Secret.Sha256())
                    },

                    RedirectUris = option.Hybrid_Alexa.RedirectUris,
                    PostLogoutRedirectUris = option.Hybrid_Alexa.PostLogoutRedirectUris,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedScopes = option.Hybrid_Alexa.Scopes,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RequireConsent = false
                },
                new Client
                {
                    ClientId = option.AuthCode_Amber.ClientId,
                    ClientName =  option.AuthCode_Amber.ClientName,
                    AllowedGrantTypes = GrantTypes.Code,

                    ClientSecrets =
                    {
                        new Secret(option.AuthCode_Amber.Secret.Sha256())
                    },

                    RedirectUris = option.AuthCode_Amber.RedirectUris,
                    PostLogoutRedirectUris = option.AuthCode_Amber.PostLogoutRedirectUris,

                    AllowedScopes = option.AuthCode_Amber.Scopes,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RequireConsent = false
                }
            };
        }
    }
}
