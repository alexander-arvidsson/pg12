
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
        public static string masterPassword;
        ClientObject clientObject = new ClientObject();

        public void CreateClient(string clientPath)
        {
            clientObject.SecretKey = SecretKeyGenerator.secretKey;
            string storeJsonString = JsonSerializer.Serialize(clientObject);
            File.WriteAllText(clientPath, storeJsonString);
            Console.WriteLine(storeJsonString);

        }

        public void CreateLoginClient(string clientPath)
        {
            string getJsonOrigin = File.ReadAllText(clientPath);
            string storeJsonString = JsonSerializer.Serialize(getJsonOrigin);
            File.WriteAllText(clientPath, storeJsonString);
        }

        public String GetDezerializedKey(string clientPath)
        {
            string jsonString = File.ReadAllText(clientPath);
            clientObject = JsonSerializer.Deserialize<ClientObject>(jsonString);

            return clientObject.SecretKey;
        }

        public void CreateMasterPassword()
        {
            Console.WriteLine("Create a new master password");
            masterPassword = Console.ReadLine();
        }
    }

}