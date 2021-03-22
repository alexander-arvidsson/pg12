using System;

namespace Lösenordshanterare_PG12
{
    public class Init
    {
        private Client client = new Client();
        private SecretKeyGenerator generator = new SecretKeyGenerator();
        private Server server = new Server();

        public void Initialize(string[] args)
        {
            generator.GenerateSecretKey();
            client.CreateClient(args[1]);
            server.CreateServer(args[2], args[1]);
        }
    }
}
