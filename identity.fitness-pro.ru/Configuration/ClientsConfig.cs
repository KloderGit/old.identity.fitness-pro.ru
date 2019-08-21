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
                    ClientId = option.FpaService.ClientId,
                    ClientName = option.FpaService.ClientName,                    
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        new Secret(option.FpaService.Secret.Sha256())
                    },
                    AllowedScopes = option.FpaService.Scopes
                },
                new Client
                {
                    ClientId = option.FpaMobile.ClientId,
                    ClientName = option.FpaMobile.ClientName,
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret(option.FpaMobile.Secret.Sha256())
                    },
                    AllowedScopes = option.FpaMobile.Scopes
                },
                new Client
                {
                    ClientId = option.FpaSite.ClientId,
                    ClientName = option.FpaSite.ClientName,
                    AllowedGrantTypes = GrantTypes.Hybrid,

                    ClientSecrets =
                    {
                        new Secret(option.FpaSite.Secret.Sha256())
                    },

                    RedirectUris = option.FpaSite.RedirectUris,
                    PostLogoutRedirectUris = option.FpaSite.PostLogoutRedirectUris,

                    AlwaysIncludeUserClaimsInIdToken = true,

                    AllowedScopes = option.FpaSite.Scopes,

                    AllowOfflineAccess = true,
                    UpdateAccessTokenClaimsOnRefresh = true,

                    RequireConsent = false
                }
            };
        }
    }
}
