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
        public static IIdentityServerBuilder AddCertificat(this IIdentityServerBuilder identityServerBuilder, bool isDevelop, IConfiguration configuration)
        {
            if (isDevelop)
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                var certificat = configuration.GetSection("PrivateConfigPath").Value;
                identityServerBuilder.AddSigningCredential(new X509Certificate2(certificat, ""));
            }

            return identityServerBuilder;
        }
    }
}
