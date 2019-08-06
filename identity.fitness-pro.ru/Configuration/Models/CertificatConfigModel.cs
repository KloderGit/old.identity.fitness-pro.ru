using System.Security.Cryptography.X509Certificates;

namespace identity.fitness_pro.ru.Configuration.Models
{
    public class CertificatConfigModel
    {
        public string FileName { get; set; }
        public string FileLocation { get; set; }
        public string Secret { get; set; }

        public X509Certificate2 GetCertificatFromFile()
        {
            var path = FileLocation + "\\" + FileName;

            return new X509Certificate2(path, Secret);
        }

        public X509Certificate2 GetCertificatFromStore(string issuer)
        {
            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);

            store.Open(OpenFlags.ReadOnly);

            return store.Certificates.Find(X509FindType.FindByIssuerName, issuer, true)[0];
        }
    }
}

//  X509Certificate2 cert = new X509Certificate2(); 
//  cert.Import(FileLocation, Secret, X509KeyStorageFlags.PersistKeySet | X509KeyStorageFlags.MachineKeySet); 
