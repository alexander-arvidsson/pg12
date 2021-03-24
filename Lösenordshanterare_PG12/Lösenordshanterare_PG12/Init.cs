
using System;

namespace Lösenordshanterare_PG12
{
    public class Init
    {
        private Client client = new Client();
        private Server server = new Server();

        public void Initialize(string[] args)
        {
            client.CreateClient(args[1]);
            server.CreateServer(args[2], args[1]);
            Console.WriteLine($"Creating a new client at: {args[1]} and server at {args[2]}");
        }
    }
}
