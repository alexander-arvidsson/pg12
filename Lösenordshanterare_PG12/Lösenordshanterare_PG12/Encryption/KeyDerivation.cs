using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12.Encryption
{
    //Seems to work... Surprisingly simple, hopefully stays working in decryption stage
    public class KeyDerivation
    {
    Client c = new Client();

        public byte[] GetVaultKey(string masterPassword, string clientPath, string secretKey = "")
        {
            byte[] salt;
            Rfc2898DeriveBytes k1;

            if (String.IsNullOrEmpty(secretKey))
            {
                salt = Convert.FromBase64String(c.GetDezerializedKey(clientPath));
                
            } else
            {
                salt = Convert.FromBase64String(secretKey);
            }

            k1 = new Rfc2898DeriveBytes(masterPassword, salt);

            return k1.GetBytes(16);
        }
    }
}
