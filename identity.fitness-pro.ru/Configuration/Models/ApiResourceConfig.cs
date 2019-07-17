using identity.fitness_pro.ru.Configuration.Interfaces;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class ApiResourceConfig : IPayload<ApiResource>
    {
        public IEnumerable<ApiResourceConfigItem> Api { get; set; }

        public IEnumerable<ApiResource> GetPayload()
        {
            return Api.Select(c => new ApiResource(c.name, c.displayName, c.claimTypes));
        }
    }

    public class ApiResourceConfigItem
    {
        public string name { get; set; }
        public string displayName { get; set; }
        public IEnumerable<string> claimTypes { get; set; }
    }
}
