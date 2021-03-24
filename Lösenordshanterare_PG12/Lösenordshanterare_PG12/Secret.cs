using System;

namespace Lösenordshanterare_PG12
{
    public class Secret
    {
        private Client client = new Client();
        public void GetSecret(string[] args)
        {
            string secretKey = client.GetDezerializedKey(args[1]);
            Console.WriteLine(secretKey);
        }
    }
}
