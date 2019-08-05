using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class IdentityConfigurationModel
    {
        public Build Build { get; set; }
    }

    public class Build
    {
        public ConfigParams Release { get; set; }
        public ConfigParams Test { get; set; }
    }

    public class ConfigParams
    {
        public Dictionary<string, string> ConnectionStrings { get; set; }
    }
}
