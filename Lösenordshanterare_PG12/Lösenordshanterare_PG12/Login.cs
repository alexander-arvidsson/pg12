using System;
using System.Collections.Generic;
using System.Text;


namespace Lösenordshanterare_PG12
{
    class Login
    {
        private Client c = new Client();
        private Server s = new Server();

        public void newLogin(string[] args)
        {
            Console.WriteLine("Enter your secret key:");
            string secretKey = Console.ReadLine();
            s.GetUnEncryptedVault(args[2], args[1], secretKey);
        }
    } 
}