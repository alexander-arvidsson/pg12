
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

        public void CreateClient()
        {
            clientObject.SecretKey = SecretKeyGenerator.secretKey;
            string storeJsonString = JsonSerializer.Serialize(clientObject);
            File.WriteAllText("client.json", storeJsonString);
            Console.WriteLine(storeJsonString);

        }

        public String GetDezerializedKey()
        {
            string jsonString = File.ReadAllText("client.json");
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