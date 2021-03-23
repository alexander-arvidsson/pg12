using System;
using System.Collections.Generic;
using System.Text;

namespace Lösenordshanterare_PG12
{
    public class Menu
    {
        public void GetMenu()
        {
            Console.WriteLine("Welcome, for using any of the following commands enter them in this format");
            Console.WriteLine("<command> [<args>] [<args>\n");
            Console.WriteLine("init - Create new vault.");
            Console.WriteLine("login - Log in to existing vault.");
            Console.WriteLine("get - Show stored values for some property or list properties in vault.");
            Console.WriteLine("set - Store value for some (possibly new) property in vault.");
            Console.WriteLine("drop - Drop some property from vault.");
            Console.WriteLine("secret - Show secret key.\n");
            Console.WriteLine("Examples:\ninit <client> <server>");
        }
    }
}
