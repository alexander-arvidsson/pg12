using System;
using System.Collections.Generic;

namespace Lösenordshanterare_PG12
{
    public class Drop
    {
        private Server server = new Server();

        public void DropProperty(string[] args)
        {

            Dictionary<string, string> keyvalue = server.GetUnEncryptedVault(args[2], args[1]);


            if (keyvalue.ContainsKey(args[3]))
            {
                keyvalue.Remove(args[3]);
                Console.WriteLine($"Removing property value: {args[3]}");
            }

            else
            {
                Console.WriteLine($"Property value {args[3]} doesn't exist in vault");
            }

            server.EncryptVaultAndWriteToServer(args[2], keyvalue, args[1]);
        }
    }
}
