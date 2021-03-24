using System;
using System.Security.Cryptography;

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
                    ErrorHandler(() => a.Initialize(args));
                    break;
                case "login":
                    Login cmdLogin = new Login();
                    ErrorHandler(() => cmdLogin.login(args));
                    break;
                case "get":
                    Get cmdGet = new Get();
                    ErrorHandler(() => cmdGet.get(args));
                    break;
                case "set":
                    Set cmdSet = new Set();
                    ErrorHandler(() => cmdSet.set(args));
                    break;
                case "drop":
                    Drop cmdDrop = new Drop();
                    ErrorHandler(() => cmdDrop.DropProperty(args));
                    break;
                case "secret":
                    Secret cmdSecret = new Secret();
                    ErrorHandler(() => cmdSecret.GetSecret(args));
                    break;
                default:
                    Console.WriteLine("Please enter a valid input");
                    break;
            }
        }
        private static void ErrorHandler(Action errorMethod)
        {
            try
            {
                errorMethod();
            }
            catch (SystemException ex)
            {
                if (ex is FormatException)
                {
                    Console.WriteLine("Incorrect secret key format, aborting command");
                }
                else if (ex is CryptographicException)
                {
                    Console.WriteLine("Decryption failed, aborting command");
                }
                else
                {
                    Console.WriteLine("Incorrect args, please check your arguments again");
                }
            }
        }

    }
}