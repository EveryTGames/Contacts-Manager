using System;
using System.Collections.Generic;
using System.Threading.Tasks;





//copy
/*
 * ______________________________________________________________________________________________
   |                                 this code is from                                          |
   |                  (https://github.com/EveryTGames/Contacts-Manager)                         |
   |                                                                                            |
   |                            ******************************                                  |
   |                            Contacts manager v2.1 BY ETG                                    |
   |                           ******************************                                   |
   |                                                                                            |
   |      this program made completly by me from 05/02/2024 till 08/02/2024 as a task for       |
   |    CAT Realoded Team anyone can reuse the code and edit it but please till me through      |
   |                             the github page at this url                                    |
   |                (https://github.com/EveryTGames/Contacts-Manager/pulls)                     |
   |                        to learn from your edits, and sorry                                 |
   |                    for the spelling mistakes in the program.                               |
   |                                                                                            |
   |                                    *********                                               |
   |                                this code is from                                           |
   |                   (https://github.com/EveryTGames/Contacts-Manager)                        |
   |                                                                                            |
   |                                                                                            |
 * |____________________________________________________________________________________________|
 */

namespace cat_task2_final
{
    internal class Program
    {
        public static List<Contact> contacts;
        static void Main(string[] args)
        {
            Console.WriteLine(@"welcome to the contacts manager program
the using is simple, each page has some options at the  top,
u need to press the corresponding key or number,
for some inputs what you right may be invisible so dont worry, you are still writing
press an key to start ...");
            Console.ReadKey();


            try
            {
                contacts = contact_manager.load();
            }
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine();
                Console.WriteLine("no save file found,creating a new save");
                Task.Delay(4000).Wait();
                contacts = new List<Contact>();
            }
            pages.show_all_contacts_page();

            Console.ReadKey();

        }
    }
}
