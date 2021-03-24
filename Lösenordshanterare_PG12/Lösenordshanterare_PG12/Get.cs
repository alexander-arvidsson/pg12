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
                } else
                {
                    Console.WriteLine($"No password exists for {args[3]}");
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Current keys in vault:");
                foreach (KeyValuePair<string, string> valuePair in vault)
                {
                    Console.WriteLine(valuePair.Key);
                }
            }

        }
    }
}
