using System;
using System.Text;


namespace cat_task2_final
{
    public static class Input
    {
        /// <summary>
        /// the index of the bottom +1 (for the curser)
        /// </summary>
        public static int by { get { return Console.WindowWidth; } }
        public static int cpx
        {
            get { return Console.CursorLeft; }
            set
            {
                try
                {
                    Console.CursorLeft = value;

                }
                catch (Exception)
                {
                    Console.WriteLine("");


                }
            }
        }

        public static int cpy
        {
            get { return Console.CursorTop; }
            set
            {
                try
                {
                    Console.CursorTop = value;

                }
                catch (Exception)
                {
                    Console.WriteLine("");


                }
            }
        }



        public static void sc(int x, int y)
        {
            Console.SetCursorPosition(x, y);
        }
        public static void wsc(string text, int x, int y)
        {

            Console.WriteLine(text);
        }



        public static void deleteelast()
        {
            cpx--;
            Console.Write(" ");
            cpx--;
        }




        /// <summary>
        /// -1 for options (instant return)
        ///    0  for integer input only
        ///1 for date input only
        ///2 for strings that doesnt allow space in it
        ///3 for tables
        ///4 for general strings
        /// 5 for addedDate
        /// </summary>
        /// <param name="type">
        /// -1 for options (instant return)
        ///  0  for integer input only
        /// 1 for date input only
        ///2 for strings that doesnt allow space in it
        ///3 for tables
        ///4 for general strings
        ///5 for addedDate
        ///</param>
        ///<param name="hidden">making what the user write hidden if true</param>
        /// <returns></returns>
        public static string handle_input(int type, bool hidden, bool escapable = false)
        {
            if (type == -1)
            {
                Console.CursorVisible = false;

            }
            else
                Console.CursorVisible = true;
            if (type == 5)
            {
                DateTime dateTime = DateTime.Now;
                string thetime = DateTime.Now.ToString("dd/MM/yyyy");
                Console.WriteLine(thetime);
                return thetime;
            }
            int date_counter = 0;
            bool on = true;
            StringBuilder input = new StringBuilder();
            ConsoleKeyInfo key;
            string temp = null;
            bool deleteable;
            while (on)
            {
                if (input.Length >= 9 && (type == 0 || type == 3))
                {
                    on = false;
                    temp = input.ToString().Substring(0, 9);
                    input.Clear();
                    return temp;

                }
                (int x, int y) = (Console.CursorLeft, Console.CursorTop);

                key = Console.ReadKey();

                if ((char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar)) || char.IsPunctuation(key.KeyChar) || char.IsSeparator(key.KeyChar))
                    deleteable = true;
                else
                    deleteable = false;

                if ((key.Key == ConsoleKey.Escape) && escapable)
                {
                    return null;



                }
                if (deleteable && hidden)
                    deleteelast();

                if (key.Key == ConsoleKey.Enter)
                {
                    Console.SetCursorPosition(x, y);
                    switch (type)
                    {
                        case 3:
                            Console.SetCursorPosition(x, y);

                            Console.Write("\t");

                            goto case 0;
                        case 2:
                        case 4:

                        case 0:
                            if (input.Length != 0)
                            {

                                on = false;
                                temp = input.ToString();
                                input.Clear();
                                return temp;
                            }
                            else
                            {
                                Console.SetCursorPosition(x, y);
                                break;
                            }

                        case 1:
                            if (date_counter >= 2 && input[input.Length - 1] != '/')
                                goto case 0;
                            else if (input.Length != 0)
                            {

                                if (input[input.Length - 1] != '/')
                                {
                                    cpx = x;
                                    if (!hidden)
                                        Console.Write("/");
                                    input.Append('/');
                                    date_counter++;
                                }
                            }
                            else
                                Console.SetCursorPosition(x, y);

                            break;




                    }


                }
                else if (key.Key == ConsoleKey.Backspace)
                {

                    Console.SetCursorPosition(x, y); //disabling the auto move back of the curser 
                    if (input.Length > 0)//doesnt allow going out of the entered text
                    {

                        if (type == 1 && input[input.Length - 1] == '/')
                        {
                            date_counter--;
                        }
                        input = input.Remove(input.Length - 1, 1);
                        if (!hidden)
                            deleteelast();

                    }
                }
                else if (key.Key == ConsoleKey.Spacebar)
                {
                    switch (type)
                    {
                        case 1:
                        case -1:


                        case 3:
                        case 2:
                        case 0:
                            deleteelast();
                            Console.Beep();
                            break;
                        case 4:
                            input.Append(key.KeyChar);
                            break;
                    }

                }
                else if (!char.IsNumber(key.KeyChar) && (type == 3 || type == 0 || type == 1 || type == -1))
                {
                    switch (type)
                    {
                        case 1:
                            if (key.KeyChar == '/')
                            {
                                if (input.Length != 0)
                                {

                                    if (input[input.Length - 1] != '/' && date_counter < 2)
                                    {

                                        date_counter++;
                                    }
                                    else
                                    {
                                        if (!hidden)
                                            deleteelast();
                                        Console.SetCursorPosition(x, y);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!hidden)
                                        deleteelast();
                                    Console.SetCursorPosition(x, y);
                                    break;
                                }
                                input.Append(key.KeyChar);
                                break;

                            }
                            if (!hidden && deleteable)
                                deleteelast();
                            Console.Beep();

                            break;

                        case -1:
                        case 3:
                        case 0:
                            if (deleteable && !hidden)
                                deleteelast();
                            Console.Beep();
                            break;
                    }


                }

                else
                {
                    input.Append(key.KeyChar);
                    if (type == -1)
                    {
                        on = false;
                        temp = input.ToString();
                        input.Clear();
                        return temp;
                    }
                }
            }
            return input.ToString();

        }
    }

}
