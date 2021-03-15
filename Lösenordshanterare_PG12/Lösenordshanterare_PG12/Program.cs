using System;
namespace Lösenordshanterare_PG12
{
    class Program

    {
        static Set s = new Set();
        static Init init = new Init();
        static void Main(string[] args)
        {

            init.Initialize();

            Console.WriteLine("Welcome, please enter a number of one of the following commands");
            Console.WriteLine("1 Init - Create new vault.");
            Console.WriteLine("2 Login - Log in to existing vault.");
            Console.WriteLine("3 Get - Show stored values for some property or list properties in vault.");
            Console.WriteLine("4 Set - Store value for some (possibly new) property in vault.");
            Console.WriteLine("5 Drop - Drop some property from vault.");
            Console.WriteLine("6 Secret - Show secret key.");

            string command = args[0];

            switch (command)
            {
                case "init":
                    Init a = new Init();
                    init = a; 
                    break;
                case "login":
                    Login b = new Login();
                    break;
                case "get":
                    // Kommer metoden för att hämta data från vault att skapas i Server klassen? Om inte hur ska jag komma åt den datan? 
                    break;
                case "set":
                    s.IsFlagged(args.Length);
                    s.set(args[1]);
                    break;
                case "drop":
                    break;
                case "secret":
                    init.Initialize();
                    break;
                default:
                    Console.WriteLine("Please enter a valid input");
                    break;                  
            }
        }
    }
}