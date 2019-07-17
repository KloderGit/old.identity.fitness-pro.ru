using IdentityServer4.Models;
using Mapster;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class ClientResourceConfig
    {
        TypeAdapterConfig config = new TypeAdapterConfig();

        public ClientResourceConfig()
        {
            config.NewConfig<IDictionary<string, string>, Dictionary<string, string>>()
                .Include<Dictionary<string, string>, Dictionary<string, string>>();

            config.NewConfig<ClientSecretConfigItem, Secret>()
                .ConstructUsing(src => new Secret(src.Value.Sha256(), null));

            config.NewConfig<ClientClaimsConfigItem, Claim>()
                .ConstructUsing(src => new Claim(src.Type, src.Value));

            config.NewConfig<ClientResourceConfigItem, Client>()
                .Ignore(dest => dest.Properties);
        }

        public IEnumerable<ClientResourceConfigItem> Clients { get; set; }

        public IEnumerable<Client> GetPayload()
        {
            var ffff = Clients.Adapt<IEnumerable<Client>>(config);

            return ffff;
        }
    }

    public class ClientResourceConfigItem : Client
    {
        public new ICollection<ClientSecretConfigItem> ClientSecrets { get; set; }
        public new ICollection<ClientClaimsConfigItem> Claims { get; set; }
        public new IDictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }

    public class ClientSecretConfigItem
    {
        public string Value { get; set; }
    }

    public class ClientClaimsConfigItem
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
