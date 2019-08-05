using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Extensions
{
    public static class IConfigurationBuilderExtensions
    {
        public static IConfigurationBuilder AddBuildTypeJson(this IConfigurationBuilder configurationBuilder, string filepath, string buildType)
        {
            configurationBuilder.AddJsonFile(filepath + $@"\IdentitySettings.{buildType}.json", true, true);

            return configurationBuilder;
        }
    }
}
