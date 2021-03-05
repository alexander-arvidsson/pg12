using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12
{
    //Generates our secret key
    public class SecretKeyGenerator
    {

        private RNGCryptoServiceProvider rngKey = new RNGCryptoServiceProvider();

        public String GenerateSecretKey()
        {
            byte[] secretKey = new byte[16];
            rngKey.GetBytes(secretKey);
            return Convert.ToBase64String(secretKey);
        }

    }
}