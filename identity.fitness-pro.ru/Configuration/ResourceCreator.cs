using identity.fitness_pro.ru.Configuration.Interfaces;
using IdentityServer4.Models;
using Mapster;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class ResourceCreator<TConfig> : IResourceCreator<TConfig>
        where TConfig : class, IPayload<Resource>, new()
    {
        public IEnumerable<T> GetResources<T>(IOptions<TConfig> config)
        {
            var configObject = config.Value;
            return configObject.GetPayload() as IEnumerable<T>;
        }
    }
}
