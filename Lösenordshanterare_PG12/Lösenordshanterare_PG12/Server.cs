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

        public void CreateServer(string serverPath, string clientPath)
        {
            string serverIV = aes.GenerateIV();

            objects.IV = serverIV;

            EncryptVaultAndWriteToServer(serverPath, vault, clientPath);
        }

        public void EncryptVaultAndWriteToServer(string serverPath, Dictionary<string, string> updatedVault, string clientPath)
        {
            string unEncryptedVault = JsonSerializer.Serialize(updatedVault);
            string encryptedVault = aes.EncryptVault(unEncryptedVault, objects.IV, clientPath);
            objects.Vault = encryptedVault;

            File.WriteAllText(serverPath, JsonSerializer.Serialize(objects));

        }

        public Dictionary<string, string> GetUnEncryptedVault(string serverPath, string clientPath, string secretKey = "")
        {
            string jsonString = File.ReadAllText(serverPath);
            objects = JsonSerializer.Deserialize<ServerObjects>(jsonString);
            byte[] encryptedVault = Convert.FromBase64String(objects.Vault);

            string unencryptedVault = aes.DecryptVault(encryptedVault, objects.IV, clientPath, secretKey);

            vault = JsonSerializer.Deserialize<Dictionary<string, string>>(unencryptedVault);    

            return vault;
        }
    }
}
