using System;
using System.Collections.Generic;

namespace Lösenordshanterare_PG12
{
    public class Drop
    {
        //Lägg in dictionary key-value par

        public Drop()
        {

            Dictionary<string, string> nyckelvärde = new Dictionary<string, string>();

            nyckelvärde.Add("user.gmail.com", "password1");
            nyckelvärde.Add("user.hotmail.com", "password2");
            nyckelvärde.Add("user.facebook.com", "password3");

            nyckelvärde["user.gmail.com"] = "lösenord1";

            Console.WriteLine("Välj vilket nyckelvärde-par du vill ta bort");
            string chosenvalue = Console.ReadLine();

            nyckelvärde.Remove(chosenvalue);

            if (nyckelvärde.ContainsKey(chosenvalue))
            {
                nyckelvärde.Remove(chosenvalue);
                Console.WriteLine($"Du tog bort{nyckelvärde[chosenvalue]}");
            }

            else
            {
                Console.WriteLine("Detta nyckelvärde-par finns inte i ditt valv.");
            }

          

            //delete chosenvalue

            /*if (alla emailinformation i valvet != chosenvalue)
            {
                Console.WriteLine("Detta värde finns inte i valvet.");

            Eller så gör man en for loop?? Funkar det i dictionary?

            }*/


        }
    }


}
