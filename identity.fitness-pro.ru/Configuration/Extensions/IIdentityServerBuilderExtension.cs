using identity.fitness_pro.ru.Configuration.Models;
using Microsoft.Extensions.DependencyInjection;

namespace identity.fitness_pro.ru.Configuration.Extensions
{
    public static class IIdentityServerBuilderExtension
    {
        public static IIdentityServerBuilder AddCertificat(this IIdentityServerBuilder identityServerBuilder, bool isDevelop, CertificatConfigModel certificatmodel)
        {
            var certificat = certificatmodel.GetCertificatFromFile();

            if (isDevelop)
            {
                identityServerBuilder.AddDeveloperSigningCredential();
            }
            else
            {
                identityServerBuilder.AddSigningCredential(certificat);
            }

            return identityServerBuilder;
        }
    }
}