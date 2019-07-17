using identity.fitness_pro.ru.Configuration.Interfaces;
using IdentityServer4.Models;
using Mapster;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class IdentityResourceConfig : IPayload<IdentityResource>
    {
        public IEnumerable<IdentityResourceConfigItem> IdentityResources { get; set; }

        public IEnumerable<IdentityResource> GetPayload()
        {
            return IdentityResources.Adapt<IEnumerable<IdentityResource>>();
        }
    }

    public class IdentityResourceConfigItem
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
    }
}
