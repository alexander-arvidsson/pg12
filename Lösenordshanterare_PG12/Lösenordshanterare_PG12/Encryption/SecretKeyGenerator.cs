using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12
{
    public class SecretKeyGenerator
    {

        private RNGCryptoServiceProvider rngKey = new RNGCryptoServiceProvider();

        public string GenerateSecretKey()
        {
            byte[] secretByte = new byte[16];
            rngKey.GetBytes(secretByte);
            return Convert.ToBase64String(secretByte);
        }

    }
}