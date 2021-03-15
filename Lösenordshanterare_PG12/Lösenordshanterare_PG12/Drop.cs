using System;
//Ska denna vara här? nedanför:
using System.Text.Json;
using System.Collections.Generic;

namespace Lösenordshanterare_PG12
{
    public class Drop
    {
        
        //Dubbelkolla att password stämmer
        //Dubbelkolla att användare är inloggad

        private Server server = new Server();

        public Drop()
        {
            Console.Write("Enter in your master password: ");
            string passwordcheck = Console.ReadLine();

            if (passwordcheck == Client.masterPassword)
            {
                Dictionary<string, string> keyvalue = server.GetUnEncryptedVault();

                /* EXEMPEL: nyckelvärde.Add("user.gmail.com", "password1");
                nyckelvärde.Add("user.hotmail.com", "password2");
                nyckelvärde.Add("user.facebook.com", "password3");

                nyckelvärde["user.gmail.com"] = "lösenord1";*/

                Console.WriteLine("Välj vilket nyckelvärde-par du vill ta bort");
                string chosenvalue = Console.ReadLine();

                if (keyvalue.ContainsKey(chosenvalue))
                {
                    keyvalue.Remove(chosenvalue);
                    Console.WriteLine($"Du tog bort{keyvalue[chosenvalue]}");
                }

                else
                {
                    Console.WriteLine("Detta nyckelvärde-par finns inte i ditt valv.");
                }

                server.EncryptVaultAndWriteToServer(keyvalue);

            }

            else
            {
                Console.WriteLine("Password is incorrect.");
            }
        }
    }


}
