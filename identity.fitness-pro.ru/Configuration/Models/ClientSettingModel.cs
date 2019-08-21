using IdentityServer4.Models;
using System.Collections.Generic;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class ClientSettingModel
    {
        public ClientSecret FpaService { get; set; }
        public ClientSecret FpaMobile { get; set; }
        public ClientUrls FpaSite { get; set; }
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
