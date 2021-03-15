using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    class Set
    {
        Server s = new Server();
        RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

        public void set(string property, string flag)
        {
            Console.WriteLine("Enter your master password");
            string mPassword = Console.ReadLine();
            string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                  "abcdefghijklmnopqrstuvwxyz" +
                                  "0123456789";
            string rngValue = "";

            
            if (mPassword.Equals(Client.masterPassword) && flag == null)
            {
                Console.WriteLine("Enter the password you would like to keep");
                string value =  Console.ReadLine();
                
                Dictionary<string, string> vault = s.GetUnEncryptedVault();

                vault.Add(property, value);

                s.EncryptVaultAndWriteToServer(vault);
            } else if (mPassword.Equals(Client.masterPassword) && flag == "-g")
            {
                while (rngValue.Length != 20)
                {
                    byte[] rngChar = new byte[1];
                    rng.GetBytes(rngChar);
                    char character = (char)rngChar[0];
                    if (alphaNumeric.Contains(character))
                    {
                        rngValue += character;
                    }
                }
                Console.WriteLine(rngValue);
            } else
            {
                Console.WriteLine("Wrong password, command aborted");
            }
        }
    }
}
