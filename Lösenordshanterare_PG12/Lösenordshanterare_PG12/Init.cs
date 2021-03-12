namespace Lösenordshanterare_PG12
{
    //Looks Ok, not sure if I really want to place keyGeneration in this class.
    public class Init
    {
        private Client client = new Client();
        private SecretKeyGenerator generator = new SecretKeyGenerator();
        private Server server = new Server();

        public void Initialize()
        {
            generator.GenerateSecretKey();
            client.CreateClient();
            server.CreateServer();
            server.Set("test", "123");
        }
    }
}
