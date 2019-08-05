using identity.fitness_pro.ru.Configuration.Extensions;
using identity.fitness_pro.ru.Configuration.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class ExternalPrivateConfigBuilder
    {
        IConfiguration config;
        IServiceCollection services;

        public ExternalPrivateConfigBuilder(string path)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(path + @"\IdentitySettings.json", true, true)
                .AddJsonFile(path + @"\ApiSettings.json", true, true)
                .AddJsonFile(path + @"\ClientSettings.json", true, true)
                .AddJsonFile(path + @"\IdentityConfiguration.json", true, true)
                .AddJsonFile(path + @"\ConnectionSettings.json", true, true);

            config = builder.Build();
        }

        public void Build(IServiceCollection services)
        {
            this.services = services;

            services.Configure<ClientSettingModel>(config);
            services.Configure<ApiSettingModel>(config);
            services.Configure<IdetitySettingModel>(config);
            services.Configure<ConnectionStringModel>(config);
            services.Configure<IdentityConfigurationModel>(config);
        }

        public IConfiguration GetConfiguration()
        {
            return config;
        }

        internal T GetConfigObject<T>() where T : class, new()
        {
            var serviceProvider = services.BuildServiceProvider();
            return serviceProvider.GetService<IOptions<T>>().Value;
        }
    }
}
