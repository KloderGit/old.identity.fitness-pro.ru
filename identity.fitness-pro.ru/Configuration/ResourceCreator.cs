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
    public class ResourceCreator<TConfig> : IResourceCreator<IOptions<TConfig>> 
        where TConfig : class, IPayload, new()
    {
        public IEnumerable<T> GetResources<T>(IOptions<TConfig> config)
        {
            var configObject = config.Value as TConfig;
            return configObject.GetPayload().Adapt<IEnumerable<T>>();
        }
    }
}
