using System;
using System.Collections.Generic;
using System.Text;

namespace Lösenordshanterare_PG12
{
    class Get
    {
        Client c = new Client();
        Server s = new Server();
        Boolean prop;

        public void get(string[] args)
        {
            Console.WriteLine("Please enter your master password: ");
            string mPassword = Console.ReadLine();
            string password = Client.masterPassword;


            if (mPassword == password)
            {
                Dictionary<string, string> vault = s.GetUnEncryptedVault();
                try
                {

                foreach(KeyValuePair<string, string> valuePair in vault)
                {
                    if (vault.ContainsKey(args[1]))
                    {
                        Console.WriteLine($"The password for {args[1]} is: {valuePair.Value}");
                    }
                }
                } catch (IndexOutOfRangeException)
                {
                    foreach (KeyValuePair<string, string> valuePair in vault)
                    {
                        Console.WriteLine(valuePair.Key);
                    }
                }
            } else
            {
                Console.WriteLine("Password incorrect, command aborted.");
            }
        }
    }
}
