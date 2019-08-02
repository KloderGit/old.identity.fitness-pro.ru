using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public class LoadExternalPrivateConfig
    {
        public IConfiguration Load(string path)
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile(path + @"\IdentitySettings.json", true, true)
                .AddJsonFile(path + @"\ApiSettings.json", true, true)
                .AddJsonFile(path + @"\ClientSettings.json", true, true)
                .AddJsonFile(path + @"\ConnectionSettings.json", true, true);
            return builder.Build();
        }
    }
}
