using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    class Set
    {
        //Without error handling so far
        Server server;

        public static void set(string[] args)
        {
            //Decrypt Vault gives json representation of the correct vault?

            string jsonVault = "decrypted vault json";

            //Deserialize vault 
            string newPassword = Console.ReadLine();
            string prop = "AmazonUsername";     //given from somewhere
            
            JsonSerializer.Deserialize<Dictionary<string, object>>(jsonVault).Add(prop, newPassword);
            


        }
    }
}
