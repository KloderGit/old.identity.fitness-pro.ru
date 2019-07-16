using identity.fitness_pro.ru.Configuration.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class IdentityResourceConfig : IPayload
    {
        public IEnumerable<IdentityResourceConfigItem> IdentityResources { get; set; }

        public Object GetPayload()
        {
            return IdentityResources;
        }
    }

    public class IdentityResourceConfigItem
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
    }
}
