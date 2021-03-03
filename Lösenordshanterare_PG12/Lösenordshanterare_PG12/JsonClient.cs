
using System.Text.Json;
using System.IO;
using System;

namespace Lösenordshanterare_PG12
{
    public class JsonClient
    {
        private Client jsonClient = new Client();

       
        public void GetUserAndPassword()
        {
            Console.WriteLine("Write your name: ");
            jsonClient.User = Console.ReadLine();
            Console.WriteLine("Write your password: ");
            jsonClient.Password = Console.ReadLine();
        }
        public void StoreJsonClient() {
            string jsonPass = JsonSerializer.Serialize(jsonClient);

            File.WriteAllText("JsonClient", jsonPass);
            Console.WriteLine(jsonPass);

        }
    }
}
