
using System;
using System.IO;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    public class ClientObjects
    {
        public string SecretKey { get; set; }

    }

    //Needs a retrieval/ deserialization method as well
    public class Client
    {
        ClientObjects objects = new ClientObjects();

        //Will be changed to a promted password when I've figured out where to put it
        public static string masterPassword = "bestInTown1337";

        public void CreateClient()
        {
            objects.SecretKey = SecretKeyGenerator.secretKey;
            string storeJsonString = JsonSerializer.Serialize(objects);
            File.WriteAllText("client.json", storeJsonString);
            Console.WriteLine(storeJsonString);

        }
    }

}