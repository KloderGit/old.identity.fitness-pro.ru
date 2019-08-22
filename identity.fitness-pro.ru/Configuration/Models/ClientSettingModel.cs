using IdentityServer4.Models;
using System.Collections.Generic;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class ClientSettingModel
    {
        public ClientSecret ClientCredential_Anna { get; set; }
        public ClientSecret ResourceOwner_Anabel { get; set; }
        public ClientUrls Hybrid_Alexa { get; set; }
        public ClientUrls AuthCode_Amber { get; set; }
    }

    public class ClientSecret
    {
        public string ClientId { get; set; }
        public string ClientName { get; set; }
        public string Secret { get; set; }
        public ICollection<string> Scopes { get; set; }
    }

    public class ClientUrls : ClientSecret
    {
        public ICollection<string> RedirectUris { get; set; }
        public ICollection<string> PostLogoutRedirectUris { get; set; }
    }
}
