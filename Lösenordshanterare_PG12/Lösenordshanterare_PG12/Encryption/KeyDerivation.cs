using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12.Encryption
{
    //Seems to work... Surprisingly simple, hopefully stays working in decryption stage
    public class KeyDerivation
    {

        public byte[] GetVaultKey(string masterPassword)
        {
            byte[] salt = Convert.FromBase64String(SecretKeyGenerator.secretKey);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(masterPassword, salt);

            return k1.GetBytes(16);
        }
    }
}
