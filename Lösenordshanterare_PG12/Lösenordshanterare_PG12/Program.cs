using System;
namespace Lösenordshanterare_PG12
{
    class Program

    {
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
         
            string command = Console.ReadLine();


            switch (command)
            {
                case "1":
                    Init a = new Init();
                    init = a; 
                    break;
                case "2":
                    Login b = new Login();
                    break;
                case "3":
                    // Kommer metoden för att hämta data från vault att skapas i Server klassen? Om inte hur ska jag komma åt den datan? 
                    break;
                case "4":
                    //Kommer metod för att ändra data i vault att skapas i Server klassen? Om inte hur tänker att datan ska manipuleras? 
                    break;
                case "5":
                    break;
                case "6":
                    init.Initialize();
                    break;
                default:
                    Console.WriteLine("Please enter a valid input");
                    break;
                        
            }
        }
    }
}
