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

        public void set(string[] args)
        {
            string alphaNumeric = "ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
                                  "abcdefghijklmnopqrstuvwxyz" +
                                  "0123456789";
            string rngValue = "";


            try {
                if (args[4] != null)
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

                    Dictionary<string, string> vault = s.GetUnEncryptedVault(args[2], args[1]);
                    if (vault.ContainsKey(args[3]))
                    {
                        vault.Remove(args[3]);
                    }
                    vault.Add(args[3], rngValue);
                    s.EncryptVaultAndWriteToServer(args[2], vault, args[1]);

                    Console.WriteLine($"Your new, secure password for {args[3]} is: {rngValue}");
                }
            } catch (IndexOutOfRangeException)
            {
                Console.WriteLine($"Enter the password you would like for {args[3]}");
                string value = Console.ReadLine();

                Dictionary<string, string> vault = s.GetUnEncryptedVault(args[2], args[1]);
                if (vault.ContainsKey(args[3]))
                {
                    vault.Remove(args[3]);
                }
                vault.Add(args[3], value);
                s.EncryptVaultAndWriteToServer(args[2], vault, args[1]);
                Console.WriteLine($"{value} has been registered as password for {args[3]}");
            }
        }
    }
}
