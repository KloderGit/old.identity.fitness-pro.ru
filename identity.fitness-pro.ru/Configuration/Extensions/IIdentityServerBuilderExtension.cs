using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration.Extensions
{
    public static class IIdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddCertificat(this IIdentityServerBuilder identityServerBuilder, bool isDevelop, string externalConfigPath)
        {
            var certif = externalConfigPath + @"\STAR_fitness-pro_ru.pfx";

            if (isDevelop)
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                var certificatFile = externalConfigPath + @"\STAR_fitness-pro_ru.pfx";
                identityServerBuilder.AddSigningCredential(new X509Certificate2(certificatFile, ""));
            }

            return identityServerBuilder;
        }
    }
}
