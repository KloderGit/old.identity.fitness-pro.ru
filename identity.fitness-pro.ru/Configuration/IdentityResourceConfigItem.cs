using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class IdentityResourceConfigItem
    {
        public string DisplayName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
    }
}
