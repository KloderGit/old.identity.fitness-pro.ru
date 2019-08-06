using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static T GetOption<T>(this IServiceCollection serviceCollection) where T : class, new()
        {
            var serviceProvider = serviceCollection.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>().Value;
        }
    }
}
