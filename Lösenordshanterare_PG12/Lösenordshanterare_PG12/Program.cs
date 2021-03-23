using System;
namespace Lösenordshanterare_PG12
{
    public class Program

    {
        public static void Main(string[] args)
        {
            string command = args[0];

            switch (command)
            {
                case "menu":
                    Menu m = new Menu();
                    m.GetMenu();
                    break;
                case "init":
                    Init a = new Init();
                    a.Initialize(args);
                    break;
                case "login":
                    Login cmdLogin = new Login();
                    cmdLogin.login(args);
                    break;
                case "get":
                    Get cmdGet = new Get();
                    cmdGet.get(args);
                    break;
                case "set":
                    Set cmdSet = new Set();
                    cmdSet.set(args);
                    break;
                case "drop":
                    Drop cmdDrop = new Drop();
                    cmdDrop.DropProperty(args);
                    break;
                case "secret":
                    Secret cmdSecret = new Secret();
                    cmdSecret.GetSecret(args);
                    break;
                default:
                    Console.WriteLine("Please enter a valid input");
                    break;
            }
        }
    }
}