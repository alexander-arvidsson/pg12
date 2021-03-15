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
        Boolean flag = new Boolean();

        public void set(string property)
        {
            Console.WriteLine("Enter your master password");
            string mPassword = Console.ReadLine();
            string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                  "abcdefghijklmnopqrstuvwxyz" +
                                  "0123456789";
            string rngValue = "";



            if (mPassword.Equals(Client.masterPassword) && flag == false)
            {
                Console.WriteLine($"Enter the password you would like for {property}");
                string value = Console.ReadLine();

                Dictionary<string, string> vault = s.GetUnEncryptedVault();

                vault.Add(property, value);

                s.EncryptVaultAndWriteToServer(vault);
            } else if (mPassword.Equals(Client.masterPassword) && flag == true)
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
                Console.WriteLine($"Your new, secure password for {property} is: {rngValue}");
            } else
            {
                Console.WriteLine("Wrong password, command aborted");
            }
        }
        public void IsFlagged(int length)
        {
            if(length == 3)
            {
                this.flag = true;
            } else
            {
                this.flag = false;
            }
        }
    }
}
