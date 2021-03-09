using System;
using System.Text;
using System.Security.Cryptography;
namespace Lösenordshanterare_PG12
{
    //This class should be good. Only lacks an decryption method
    public class AesEncryptor
    {
        private AesCryptoServiceProvider aesCrypto = new AesCryptoServiceProvider();
        private Encryption.KeyDerivation derivateKey = new Encryption.KeyDerivation();

        public String EncryptVault(string vault, string IV)
        {
            aesCrypto.IV = Convert.FromBase64String(IV);
            aesCrypto.Key = derivateKey.GetVaultKey();

            ICryptoTransform transform = aesCrypto.CreateEncryptor(aesCrypto.Key, aesCrypto.IV);

            byte[] encrypt = transform.TransformFinalBlock(Encoding.ASCII.GetBytes(vault), 0, vault.Length);

            string encryptedVault = Convert.ToBase64String(encrypt);

            return encryptedVault;

        }

        public String GenerateIV()
        {
            aesCrypto.GenerateIV();
            return Convert.ToBase64String(aesCrypto.IV);
        }
    }
}
