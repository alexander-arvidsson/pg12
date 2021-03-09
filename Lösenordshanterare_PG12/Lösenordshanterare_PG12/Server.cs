using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Lösenordshanterare_PG12
{
    public class ServerObjects
    {
        public string IV { get; set; }
        public Dictionary<string, object> Vault { get; set; }

    }

    public class Server
    {

        //Might have to split this up into further methods for the retrieval
        public void CreateServer()
        {
            ServerObjects objects = new ServerObjects();
            AesEncryptor aes = new AesEncryptor();

             objects.IV = aes.GenerateIV();

            //TODO: Find a nicer way to keep the JsonFormatting
            string unEncryptedVault = JsonSerializer.Serialize(objects.Vault);
            string IVKey = JsonSerializer.Serialize(objects.IV);

            string encryptedVault = aes.EncryptVault(unEncryptedVault, objects.IV);

            File.WriteAllText("server.json", IVKey);
            File.AppendAllText("server.json", encryptedVault);

            string readAll = File.ReadAllText("server.json");

            //for testing purposes
            Console.WriteLine(readAll);
        }

    }
}
