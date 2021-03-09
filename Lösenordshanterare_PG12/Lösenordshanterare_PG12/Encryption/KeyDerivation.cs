using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12.Encryption
{
    //Seems to work... Surprisingly simple, hopefully stays working in decryption stage
    public class KeyDerivation
    {

        private Client client = new Client();

        public byte[] GetVaultKey()
        {
            byte[] salt = Convert.FromBase64String(SecretKeyGenerator.secretKey);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(client.masterPassword, salt);

            //Testing
            Console.WriteLine("Vault key: " + Convert.ToBase64String(k1.GetBytes(16)));

            return k1.GetBytes(16);
        }
    }
}
