namespace Lösenordshanterare_PG12
{
    public class Init
    {
        private Client client = new Client();
        private Server server = new Server();

        public void Initialize()
        {
            client.CreateClient();
            server.CreateServer();
        }
    }
}
