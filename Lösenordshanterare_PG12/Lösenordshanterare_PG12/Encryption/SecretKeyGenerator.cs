using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12
{
    //Generates our secret key
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