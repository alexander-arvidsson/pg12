using System;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;

namespace Lösenordshanterare_PG12
{
    public class ServerObjects
    {
        public string IV { get; set; }
        public string Vault { get; set; }

    }

    public class Server
    {
        public void CreateServer()
        {
            ServerObjects objects = new ServerObjects();
            AesEncryptor aes = new AesEncryptor();
            string unencryptedVault = "asdasd";

            objects.IV = aes.GenerateIV();
            objects.Vault = aes.EncryptVault(unencryptedVault, objects.IV);

            string storeJsonString = JsonSerializer.Serialize(objects);

            File.WriteAllText("server.json", storeJsonString);
            //for testing purposes
            Console.WriteLine(storeJsonString);
        }

    }
}
