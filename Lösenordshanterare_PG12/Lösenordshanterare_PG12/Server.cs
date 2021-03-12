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
        private Dictionary<string, string> vault = new Dictionary<string, string>();
        private ServerObjects objects = new ServerObjects();
        private AesEncryptor aes = new AesEncryptor();

        //Might have to split this up into further methods for the retrieval
        public void CreateServer()
        {
            serverIV = aes.GenerateIV();

            objects.IV = serverIV;
            objects.Vault = GetEncryptedVault();

            File.WriteAllText("server.json", JsonSerializer.Serialize(objects));

            string readAll = File.ReadAllText("server.json");

            //for testing purposes
            Console.WriteLine(readAll);

            GetUnEncryptedVault();
        }

        public String GetEncryptedVault()
        {
            string unEncryptedVault = JsonSerializer.Serialize(vault);
            string encryptedVault = aes.EncryptVault(unEncryptedVault, objects.IV);

            return encryptedVault;
        }

        public Dictionary<string, string> GetUnEncryptedVault()
        {
            string jsonString = File.ReadAllText("server.json");
            objects = JsonSerializer.Deserialize<ServerObjects>(jsonString);
            byte[] encryptedVault = Convert.FromBase64String(objects.Vault);

            string unencryptedVault = aes.DecryptVault(encryptedVault, objects.IV);

            vault = JsonSerializer.Deserialize<Dictionary<string, string>>(unencryptedVault);

            //For testing, can be removed 
            if (vault.Count == 0)
            {
                Console.WriteLine("No current content in vault");
            } else
            {
                foreach (KeyValuePair<string, string> valuePair in vault)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", valuePair.Key, valuePair.Value);
                }
            }

            return vault;
        }

        public void Set(string prop, string value)
        {
            GetUnEncryptedVault().Add(prop, value);
        }
    }
}
