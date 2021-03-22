using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12.Encryption
{
    //Seems to work... Surprisingly simple, hopefully stays working in decryption stage
    public class KeyDerivation
    {
    Client c = new Client();

        public byte[] GetVaultKey(string masterPassword, string clientPath)
        {
            byte[] salt = Convert.FromBase64String(c.GetDezerializedKey(clientPath));
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(masterPassword, salt);

            return k1.GetBytes(16);
        }

        public byte[] GetVaultKey(string masterPassword, string clientPath, string secretKey)
        {
            byte[] salt = Convert.FromBase64String(secretKey);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(masterPassword, salt);

            return k1.GetBytes(16);
        }
    }
}
