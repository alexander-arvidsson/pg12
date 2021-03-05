using System;
using System.Collections.Generic;
using System.Text;

namespace Lösenordshanterare_PG12
{
    class Login
    {
        private string pwd;
        private string ckret;
        private JsonClient c = new JsonClient();
        private Server s;

        public Login (JsonClient jsonClient, Server server, string password, string secret)
        {
            pwd = password;
            ckret = secret;
            s = server;
            c = jsonClient;
        }
    }
}
