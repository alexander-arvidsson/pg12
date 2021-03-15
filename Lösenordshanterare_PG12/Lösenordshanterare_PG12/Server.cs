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
        private Dictionary<string, string> vault = new Dictionary<string, string>();
        private ServerObjects objects = new ServerObjects();
        private readonly AesEncryptor aes = new AesEncryptor();

        //Might have to split this up into further methods for the retrieval

        public void CreateServer()
        {
            string serverIV = aes.GenerateIV();

            objects.IV = serverIV;

            EncryptVaultAndWriteToServer(vault);
            GetUnEncryptedVault();
        }

        public void EncryptVaultAndWriteToServer(Dictionary<string, string> updatedVault)
        {
            string unEncryptedVault = JsonSerializer.Serialize(updatedVault);
            string encryptedVault = aes.EncryptVault(unEncryptedVault, objects.IV);
            objects.Vault = encryptedVault;

            File.WriteAllText("server.json", JsonSerializer.Serialize(objects));

            string readAll = File.ReadAllText("server.json");

            //for testing purposes
            Console.WriteLine(readAll);

            //For testing, can be removed 
            if (vault.Count == 0)
            {
                Console.WriteLine("No current content in vault");
            }
            else
            {
                foreach (KeyValuePair<string, string> valuePair in vault)
                {
                    Console.WriteLine("Key = {0}, Value = {1}", valuePair.Key, valuePair.Value);
                }
            }


        }

        public Dictionary<string, string> GetUnEncryptedVault()
        {
            string jsonString = File.ReadAllText("server.json");
            objects = JsonSerializer.Deserialize<ServerObjects>(jsonString);
            byte[] encryptedVault = Convert.FromBase64String(objects.Vault);

            string unencryptedVault = aes.DecryptVault(encryptedVault, objects.IV);

            vault = JsonSerializer.Deserialize<Dictionary<string, string>>(unencryptedVault);

            

            return vault;
        }
    }
}
