using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;

namespace Lösenordshanterare_PG12
{
    class Set
    {
        Server s = new Server();
        public void set(string[] args)
        {
            Dictionary<string, string> vault = s.GetUnEncryptedVault();

            vault.Add("google.com", "password123");

        }
    }
}
