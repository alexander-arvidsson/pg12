using System;
using System.Collections.Generic;
using System.Text;

namespace Lösenordshanterare_PG12
{
    class Get
    {
        Client c = new Client();
        Server s = new Server();
        public void get(string[] args)
        {
            
                Dictionary<string, string> vault = s.GetUnEncryptedVault(args[2], args [1]);
                try
                {

                foreach(KeyValuePair<string, string> valuePair in vault)
                {
                    if (vault.ContainsKey(args[3]))
                    {
                        Console.WriteLine($"The password for {args[3]} is: {valuePair.Value}");
                    }
                }
                } catch (IndexOutOfRangeException)
                {
                    foreach (KeyValuePair<string, string> valuePair in vault)
                    {
                        Console.WriteLine(valuePair.Key);
                    }
                }

            }
        }
    }
