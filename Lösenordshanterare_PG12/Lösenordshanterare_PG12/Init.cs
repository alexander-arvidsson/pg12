using System;

namespace Lösenordshanterare_PG12
{
    public class Init
    {
        private Client client = new Client();
        private SecretKeyGenerator generator = new SecretKeyGenerator();
        private Server server = new Server();

        public void Initialize()
        {
            client.CreateMasterPassword();
            generator.GenerateSecretKey();
            client.CreateClient();
            server.CreateServer();
            server.Set("test", "123");
        }
    }
}
