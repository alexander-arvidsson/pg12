using System;

namespace Lösenordshanterare_PG12
{
    class Program

    {
        static void Main(string[] args)

        {
            JsonClient jsonClient = new JsonClient();

            jsonClient.GetUserAndPassword();
            jsonClient.StoreJsonClient();
        
        }
    }
}
