using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    class Set
    {
        Server s = new Server();
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public void set(string property)
        {
            Console.WriteLine("Enter your master password");
            string mPassword = Console.ReadLine();

            byte[] rngSecret = new byte[20];


            if (mPassword.Equals(Client.masterPassword))
            {
                Console.WriteLine("Enter the password you would like to keep");
                string value =  Console.ReadLine();
            
                Dictionary<string, string> vault = s.GetUnEncryptedVault();

                vault.Add(property, value);

                s.EncryptVaultAndWriteToServer(vault);
            } else
            {
                Console.WriteLine("Wrong password, command aborted");
            }
        }
    }
}
