using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Lösenordshanterare_PG12
{
    public class ServerObjects
    {
        public string IV { get; set; }
        public string Vault { get; set; }
    }
    public class Server
    {
        private string serverIV;
        private Dictionary<string, object> vault = new Dictionary<string, object>();
        private ServerObjects objects = new ServerObjects();
        private AesEncryptor aes = new AesEncryptor();

        //Might have to split this up into further methods for the retrieval
        public void CreateServer()
        {
            serverIV = aes.GenerateIV();

            objects.IV = serverIV;
            objects.Vault = EncryptVault();

            File.WriteAllText("server.json", JsonSerializer.Serialize(objects));

            string readAll = File.ReadAllText("server.json");

            //for testing purposes
            Console.WriteLine(readAll);

            UnEncryptVault();
        }

        public String EncryptVault()
        {
            string unEncryptedVault = JsonSerializer.Serialize(vault);
            string encryptedVault = aes.EncryptVault(unEncryptedVault, objects.IV);

            return encryptedVault;
        }

        public String UnEncryptVault()
        {
            string jsonString = File.ReadAllText("server.json");
            objects = JsonSerializer.Deserialize<ServerObjects>(jsonString);
            byte[] encryptedVault = Convert.FromBase64String(objects.Vault);

            string unencryptedVault = aes.DecryptVault(encryptedVault, objects.IV);
            return unencryptedVault;
        }

    }
}
