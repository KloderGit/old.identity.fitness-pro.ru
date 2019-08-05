﻿using Microsoft.Extensions.Configuration;
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
            var certificatFile = externalConfigPath + @"\STAR_fitness-pro_ru.pfx";

            if (isDevelop)
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                identityServerBuilder.AddSigningCredential(new X509Certificate2(certificatFile, ""));
            }

            return identityServerBuilder;
        }
    }
}

//  X509Certificate2 cert = new X509Certificate2(); cert.Import(certificateFilePath, certPasshrase, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet); 
