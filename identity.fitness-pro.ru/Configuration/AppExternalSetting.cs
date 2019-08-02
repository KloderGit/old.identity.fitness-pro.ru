using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public static class AppExternalSetting
    {
        public static IConfiguration LoadSettings(string path)
        {
            var sdf = path + @"\IdentitySettings.json";

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(@"\IdentitySettings.json", true, true)
                .AddJsonFile(@"\ApiSettings.json", true, true)
                .AddJsonFile(@"\ClientSettings.json", true, true)
                .AddJsonFile(@"\ConnectionSettings.json", true, true);
            var res = builder.Build();
            return res;
        }
    }
}
