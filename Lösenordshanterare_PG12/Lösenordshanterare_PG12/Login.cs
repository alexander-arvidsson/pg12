using System;
using System.Collections.Generic;
using System.Text;


namespace Lösenordshanterare_PG12
{
    class Login
    {
        private Server s = new Server();
        private Client c = new Client();
        private ClientObject co = new ClientObject();

        public void masterLogin()
        {
            c.CreateClientLogin();
            Console.WriteLine("Enter your master password");
            string mPassword = Console.ReadLine();
            Console.WriteLine("Enter your secret key");
            string secretKey = Console.ReadLine();

            if (mPassword.Equals(Client.masterPassword) && secretKey.Equals(co.SecretKey))
            {
                Console.WriteLine("Log in successful");
            } else
            {
                Console.WriteLine("Wrong password or secret key, log in aborted");
            }

        }
    } 
}