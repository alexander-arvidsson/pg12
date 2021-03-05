
using System;
using System.IO;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    public class ClientObjects
    {
        public string SecretKey { get; set; }

    }

    public class Client
    {
        //Starting mPassword not sure if best left blank
        public string masterPassword = "bestInTown1337";
        ClientObjects objects = new ClientObjects();
        private SecretKeyGenerator generator = new SecretKeyGenerator();

        public void CreateClient()
        {
            objects.SecretKey = generator.GenerateSecretKey();
            string storeJsonString = JsonSerializer.Serialize(objects);
            File.WriteAllText("client.json", storeJsonString);
            Console.WriteLine(storeJsonString);

        }
    }

}