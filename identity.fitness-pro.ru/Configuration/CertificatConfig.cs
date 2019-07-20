using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace identity.fitness_pro.ru.Configuration
{
    public static class CertificatConfig
    {
        public static X509Certificate2 GetCertificateFromStore()
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);

            return store.Certificates.Find(X509FindType.FindByIssuerName, "COMODO", true)[0];
        }
    }
}
