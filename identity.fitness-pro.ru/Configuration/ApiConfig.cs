using identity.fitness_pro.ru.Configuration.Models;
using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public static class ApiConfig
    {
        public static IEnumerable<ApiResource> GetApis(IEnumerable<ResourceModel> items)
        {
            return items.Select(item => new ApiResource(item.Name, item.DisplayName, item.UserClaims));
        }
    }
}
