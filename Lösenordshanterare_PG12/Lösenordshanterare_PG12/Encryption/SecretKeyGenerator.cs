using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12
{
    //Generates our secret key
    public class SecretKeyGenerator
    {

        private RNGCryptoServiceProvider rngKey = new RNGCryptoServiceProvider();
        /* 
         * Is there a better way other than keeping this static? 
         * I need to pass this value around without generating it through new objects...
         */
        public static string secretKey;

        public void GenerateSecretKey()
        {
            byte[] secretByte = new byte[16];
            rngKey.GetBytes(secretByte);
            secretKey = Convert.ToBase64String(secretByte);
        }

    }
}