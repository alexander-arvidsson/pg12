using System;

namespace Lösenordshanterare_PG12
{
    public class Login
    {
        private Server s = new Server();
        private Client c = new Client();

        public void login(string[] args)
        {

            Console.WriteLine("Enter your secret key:");
            string secretKey = Console.ReadLine();
            s.GetUnEncryptedVault(args[2], args[1], secretKey);
            Console.WriteLine($"Creating a client at: {args[1]} for server: {args[2]}");
            c.CreateClient(args[1], secretKey);


        }
    }
}