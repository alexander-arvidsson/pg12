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

        public void set(string[] args)
        {
            Console.WriteLine("Enter your master password");
            string mPassword = Console.ReadLine();
            string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                  "abcdefghijklmnopqrstuvwxyz" +
                                  "0123456789";
            string rngValue = "";



            try {
                if (mPassword.Equals(Client.masterPassword) && args[2] != null)
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
                        Console.WriteLine($"Your new, secure password for {args[1]} is: {rngValue}");
                } else
                {
                    Console.WriteLine("Wrong password, command aborted");
                }
            } catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Enter the password you would like for {args[1]}");
                string value = Console.ReadLine();

                Dictionary<string, string> vault = s.GetUnEncryptedVault();

                vault.Add(args[1], value);

                s.EncryptVaultAndWriteToServer(vault);
            }
        }
    }
}
