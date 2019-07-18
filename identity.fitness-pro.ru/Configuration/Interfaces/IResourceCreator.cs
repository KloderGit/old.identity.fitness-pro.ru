using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Interfaces
{
    public interface IResourceCreator<TConfig> where TConfig : class, new()
    {
        IEnumerable<T> GetResources<T>(IOptions<TConfig> config);
    }
}
