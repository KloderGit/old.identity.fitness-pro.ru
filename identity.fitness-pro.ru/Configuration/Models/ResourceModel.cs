using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class ResourceModel
    {
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public IEnumerable<string> UserClaims { get; set; }
    }
}
