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
    public class IdentityResourceCreator<TConfig, TResource> : IResourceCreator<TConfig, TResource> where TConfig : class, new()
    {
        public IEnumerable<TResource> GetResources(IOptions<TConfig> options)
        {
            return options.Value.Adapt<IEnumerable<TResource>>();
        }
    }
}
