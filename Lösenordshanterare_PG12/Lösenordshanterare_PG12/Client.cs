
using System;
using System.IO;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    public class ClientObject
    {
        public string SecretKey { get; set; }

    }

    public class Client
    {
        private ClientObject clientObject = new ClientObject();
        private SecretKeyGenerator keyGenerator = new SecretKeyGenerator();

        public void CreateClient(string clientPath, string secretKey = "")
        {
            if (String.IsNullOrEmpty(secretKey))
            { 
                clientObject.SecretKey = keyGenerator.GenerateSecretKey(); 
            }
            else 
            { 
                clientObject.SecretKey = secretKey; 
            }
            string storeJsonString = JsonSerializer.Serialize(clientObject);
            File.WriteAllText(clientPath, storeJsonString);

        }

        public String GetDezerializedKey(string clientPath)
        {
            string jsonString = File.ReadAllText(clientPath);
            clientObject = JsonSerializer.Deserialize<ClientObject>(jsonString);

            return clientObject.SecretKey;
        }
    }

}