using identity.fitness_pro.ru.Configuration.Models;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentities(IEnumerable<ResourceModel> items)
        {
            return items.Select(item => new IdentityResource(item.Name, item.DisplayName, item.UserClaims));
        }
    }
}
