using System;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12.Encryption
{
    public class KeyDerivation
    {

        private Client client = new Client();
        private ClientObjects objects = new ClientObjects();

        public byte[] GetVaultKey()
        {
            byte[] salt = Convert.FromBase64String(objects.SecretKey);
            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(client.masterPassword, salt);

            return k1.GetBytes(16);
        }
    }
}
