using System;
using System.Collections.Generic;


namespace cat_task2_final
{

    class pages
    {
        /// <summary>
        /// making it easier to get the options input 
        ///  0  for integer input only
        /// 1 for date input only
        ///2 for strings that doesnt allow space in it
        ///3 for tables
        ///4 for general strings
        /// </summary>
        /// <param name="type">
        /// 0  for integer input only
        /// 1 for date input only
        ///2 for strings that doesnt allow space in it
        ///3 for tables
        ///4 for general strings</param>
        ///<param name="hidden">true for makeing what the user writes hidden on the screen</param>
        ///<param name="escapable">true to make the user can press Esc buttton to cancel the input</param>
        /// <returns></returns>
        public static string option(int type, bool hidden, bool escapable = false)
        {
            string x = Input.handle_input(type, hidden, escapable);
            if (x == null)
            {

                show_all_contacts_page();
            }
            Console.WriteLine();
            return x;
        }
        public static void show_all_contacts_page()
        {

            Console.Clear();
            Console.Clear();
            Console.Clear();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            contact_manager.save(Program.contacts);

            Console.WriteLine("----------------------show all contacts page--------------------------");
            Console.WriteLine("[1]open contact    [2]add contact      [3]delete contact  [4]search in all contacts      [5]quit the program   ");
            Console.WriteLine("-----------------------------------------------");


            for (int i = 0; i < Program.contacts.Count; i++)
            {
                Console.Write($"contact({i}): ");
                Console.WriteLine($"({Program.contacts[i].count}) users in it");

            }
            Console.WriteLine("-----------------------------------------------");

        aa: int ans = int.Parse(option(-1, true));
            switch (ans)
            {
                case 1:
                    Console.Write("enter the index for the contact you want to open (it starts from 0)");
                    ans = int.Parse(option(0, false));
                    if (ans >= Program.contacts.Count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {
                        Contact _contact = Program.contacts[ans];
                        show_all_users_page(ref _contact);
                    }

                    break;
                case 2:
                    Console.Write("enter the number of users you want to add now in this contact (you still can edit it later)");
                    ans = int.Parse(option(0, false));
                    Program.contacts.Add(new Contact(ans));
                    show_all_contacts_page();
                    break;
                case 3:
                    Console.Write("enter the index of the contact you want to delete (it starts from 0)");
                    ans = int.Parse(option(0, false));
                    if (ans >= Program.contacts.Count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {

                        Program.contacts.RemoveAt(ans);
                        show_all_contacts_page();
                    }
                    break;
                case 4:
                    search_page();
                    break;
                case 5:
                    Environment.Exit(0);
                    break;
                default:
                    goto aa;
            }



        }


        public static void show_all_users_page(ref Contact _contact)
        {
            Console.Clear();

            coloring();

            contact_manager.save(Program.contacts);


            Console.WriteLine("----------------------show all users page--------------------------");
            Console.WriteLine("[1]select user to edit    [2]add user      [3]delete user   [4]search in this contact  [5]go back");
            Console.WriteLine("-----------------------------------------------");

            _contact.showAll();


            Console.WriteLine("-----------------------------------------------");
        aa: int ans = int.Parse(option(-1, true));
            switch (ans)
            {
                case 1:
                    Console.Write("enter the index for the user you want to open (it starts from 0)");
                    ans = int.Parse(option(0, false));
                    if (ans >= _contact.count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {
                        user _user_ = _contact.users[ans];
                        show_all_user_data_page(ref _contact, ref _user_);
                    }

                    break;
                case 2:
                    _contact.addUser();
                    show_all_users_page(ref _contact);
                    break;
                case 3:
                    Console.Write("enter the index of the user you want to delete (it starts from 0)");
                    ans = int.Parse(option(0, false));
                    if (ans >= _contact.count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {
                        _contact.removeUser(ans);
                        show_all_users_page(ref _contact);
                    }
                    break;
                case 4:
                    search_page(_contact);
                    break;
                case 5:
                    show_all_contacts_page();
                    break;
                default:
                    goto aa;
            }



        }

        public static void show_all_user_data_page(ref Contact _contact, ref user _user_)
        {
            Console.Clear();

            coloring();

            contact_manager.save(Program.contacts);
            Console.WriteLine("----------------------show all user data page--------------------------");
            Console.WriteLine("[1]select category to edit    [2]go back");
            Console.WriteLine("available categories \"id\", \"firstname\", \"lastname\", \"gender\", \"city\", \"addedDate\", \"phones\", \"emails\", \"adresses\" ");
            Console.WriteLine("-----------------------------------------------");


            _user_.ToString(_contact.users.IndexOf(_user_));
            Console.WriteLine("-----------------------------------------------");
        aa: int ans = int.Parse(option(-1, true));
            switch (ans)
            {
                case 1:
                    Console.Write("enter the category name it must be the same spell and case sensetive(for the arrayed data will get the option after this)");
                bb: string category_name = option(2, false);
                    bool exist = false;
                    foreach (string x in user.allVariables)
                    {
                        if (x == category_name)
                            exist = true;

                    }
                    if (!exist)
                    {
                        Console.WriteLine("please enter a valid category");
                        goto bb;
                    }
                    if (category_name.EndsWith("s"))
                    {
                        edit_the_array_page(ref _contact, ref _user_, category_name);
                    }
                    else
                    {//you can here make something to control what he enters for each categeory type i think, idk just reminder
                        //and also remmeber u can use enums to make it faster
                        Console.WriteLine($"enter what you want to edit {category_name} to");
                        string value = option(user.categories[category_name], false);

                        _user_.edit(category_name, value);


                    }
                    show_all_user_data_page(ref _contact, ref _user_);

                    break;
                case 2:
                    show_all_users_page(ref _contact);
                    break;
                default:
                    goto aa;
            }



        }
        static void coloring()//just to change the color betwean pages to give more readability
        {
            if (Console.ForegroundColor == ConsoleColor.Green)
                Console.ForegroundColor = ConsoleColor.Red;
            else
                Console.ForegroundColor = ConsoleColor.Green;

        }

        public static void edit_the_array_page(ref Contact _contact, ref user _user_, string selected_category)
        {
            Console.Clear();

            coloring();

            Console.WriteLine("----------------------edit the array page--------------------------");
            Console.WriteLine("[1]edit a data type   [2]add a data type    [3]delete a data type   [Esc]cancel");
            Console.WriteLine("-----------------------------------------------");

            user _user = _user_;
            foreach (dynamic data_type_object in _user[selected_category, typeof(user)].result)
            {
                data_type_object.ToString();
            }
            Console.WriteLine("-----------------------------------------------");


        aa: int ans = int.Parse(option(-1, true, true));
            switch (ans)
            {
                case 1:
                    Console.WriteLine("please enter the index of the data type you want to edit(it starts  from 0)");
                    int index_of_the_desired_data_type = int.Parse(option(0, false, true));
                    dynamic x;
                    Type type_of_elements;
                    (x, type_of_elements) = _user[selected_category, typeof(user)];
                    if (ans >= x.Count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {

                        Console.WriteLine("which variable u want to edit in this data type?(please type it exactly as it)");
                    bb: string what_want_to_edit_in_this_data_type = option(2, false, true);
                        if (!user.categories.TryGetValue(what_want_to_edit_in_this_data_type, out int _type))
                        {
                            Console.WriteLine("variable not found,try again");
                            goto bb;
                        }
                        else if (type_of_elements.GetProperty(what_want_to_edit_in_this_data_type) == null)//to prevent the user from electing a variable that is not in the class like email is not in the  Phone class
                        {
                            Console.WriteLine($"please remember that {what_want_to_edit_in_this_data_type} is not in the {type_of_elements.Name}");
                            goto bb;
                        }



                        Console.WriteLine("enter the value that you want to change the variable to");
                        string value = option(_type, false, true);
                        _user.editUser(selected_category, index_of_the_desired_data_type, what_want_to_edit_in_this_data_type, value, _user_);
                        show_all_user_data_page(ref _contact, ref _user_);
                    }
                    break;
                case 2:
                    Console.WriteLine("enter the number of things u want to add");
                    int n_added_obj = int.Parse(option(0, false, true));
                    _user.addTo(selected_category, n_added_obj);
                    show_all_user_data_page(ref _contact, ref _user_);

                    break;
                case 3:
                cc: Console.WriteLine("enter the number of things u want to delete");
                    int n = int.Parse(option(0, false, true));
                    if (n > _user[selected_category, typeof(user)].result.Count)
                    {
                        Console.WriteLine("what u entered is larger than the number of elements,try again");
                        goto cc;
                    }
                dd: Console.WriteLine("enter the indexes of things u want to delete(it starts from zero and press enter after each index)");


                    int[] n_removed_obj = new int[n];
                    for (int i = 0; i < n; i++)
                    {
                        n_removed_obj[i] = int.Parse(option(0, false, true));
                        if (n_removed_obj[i] >= _user[selected_category, typeof(user)].result.Count)
                        {
                            Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0,enter the option again");
                            goto dd;
                        }
                    }
                    _user.delete(selected_category, n_removed_obj);
                    show_all_user_data_page(ref _contact, ref _user_);
                    break;




                default: goto aa;

            }






        }
        public static void search_page(Contact _contact = null)//if not null it means that the search is in a single contact
        {
            Console.Clear();

            coloring();

            Console.WriteLine("----------------------search page--------------------------");
            Console.WriteLine("[0]search again      [1]open a contact(if searched in all contacts)      [2]open user        [Esc]cancel");
        dd: Console.WriteLine("-----------------------------------------------------------------------------------------------------");
            Console.WriteLine("please enter the value u want to search for");
            string value = option(4, false, true);
            List<user> temp = new List<user>();
            List<List<user>> _contacts = new List<List<user>>();

            if (_contact == null)
            {
                for (int i = 0; i < Program.contacts.Count; i++)
                {
                    temp = Program.contacts[i].searchInAllUsers(value);

                    Console.Write($"contact({i}): ");
                    if (temp == null)
                    {
                        Console.WriteLine("nothing found");
                        _contacts.Add(temp);
                    }
                    else
                    {

                        Console.WriteLine($"({temp.Count}) users matched in it");

                        _contacts.Add(temp); //saves the list of the users in the corresponding index (with the contact)
                        Console.WriteLine("-----------------------------------------------");
                        foreach (user _user in temp)
                        {
                            _user.ToString(temp.IndexOf(_user));

                        }
                        Console.WriteLine("------------------------------------------------------");
                    }
                }
                if (_contacts.Count == 0)
                    Console.WriteLine("nothing found");

            }
            else
            {
                temp = _contact.searchInAllUsers(value);
                if (temp == null)
                    Console.WriteLine("nothing found");
                else
                    foreach (user _user in temp)
                    { _user.ToString(temp.IndexOf(_user)); }
            }





        aa: int ans = int.Parse(option(-1, true, true));
            switch (ans)
            {
                case 0:
                    goto dd;

                case 1:
                    if (_contact != null)
                    {
                        Console.WriteLine("you searched in a contact,no contacts to open,chooose one of the options again");
                        goto aa;
                    }
                    Console.Write("enter the index for the contact you want to open (it starts from 0)");
                    ans = int.Parse(option(0, false, true));
                    if (ans >= _contacts.Count)
                    {
                        Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {
                        Contact c_contact = Program.contacts[ans];
                        show_all_users_page(ref c_contact);
                    }

                    break;
                case 2:
                    if (_contact == null)
                    {


                        Console.Write("enter the index for the contact then press enter then the user you want to open (it starts from 0 for each contact)");
                        int x = int.Parse(option(0, false, true));
                        if (_contacts[x] == null)
                        {
                            Console.WriteLine("no users found in this contact, u cant open a user from it from the search, choose one of the options again");
                            goto aa;
                        }
                        if (x >= _contacts.Count)
                        {
                            Console.WriteLine("not found, please remember the index starts from 0, choose one of the options again");
                            goto aa;
                        }
                        else
                        {
                        bb: Console.WriteLine("enter the index for the user you want to open in this contact");
                            ans = int.Parse(option(0, false, true));
                          
                            if (ans >= _contacts[x].Count)
                            {
                                Console.WriteLine("not found, please remember the index starts from 0, try again");
                                goto bb;
                            }
                            Contact contact = Program.contacts[x];
                            user _user = _contacts[x][ans];
                            show_all_user_data_page(ref contact, ref _user);
                        }

                    }
                    else
                    {
                    bb: Console.WriteLine("enter the index for the user you want to open in this contact");
                        ans = int.Parse(option(0, false, true));
                        if (ans >= _contact.users.Count)
                        {
                            Console.WriteLine("not found, please remember the index starts from 0, try again");
                            goto bb;
                        }
                       
                      user _user =  temp[ans];
                        show_all_user_data_page(ref _contact, ref _user);

                    }
                    break;

                default:
                    goto aa;
            }




        }







    }















}

