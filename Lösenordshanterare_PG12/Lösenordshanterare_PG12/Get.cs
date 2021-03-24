using System;
using System.Collections.Generic;
using System.Text;

namespace Lösenordshanterare_PG12
{
    public class Get
    {
        private Server s = new Server();
        public void get(string[] args)
        {
            string value;
            Dictionary<string, string> vault = s.GetUnEncryptedVault(args[2], args[1]);
            try
            {
                if (vault.TryGetValue(args[3], out value))
                {
                    Console.WriteLine($"The password for {args[3]} is: {value}");
                }
                else
                {
                    Console.WriteLine($"property {args[3]} doesn't exist in vault");
                }
            }
            catch (IndexOutOfRangeException)
            {
                if (vault.Count == 0)
                {
                    Console.WriteLine("No properties exists in vault");
                }
                else
                {
                    Console.WriteLine("Current properties in vault:");
                    foreach (KeyValuePair<string, string> valuePair in vault)
                    {
                        Console.WriteLine(valuePair.Key);
                    }
                }
            }
        }
    }
}
