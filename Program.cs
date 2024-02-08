using input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;







/*
 * _____________________________________________________________________________________________
   |                                 this code is from                                          |
   |                  (https://github.com/EveryTGames/Contacts-Manager)                         |
   |                           ******************************                                   |
   |                            Contacts manager v2.1 BY ETG                                    |
   |      this program made completly by me from 05/02/2024 till 08/02/2024 as a task for       |
   |    CAT Realoded Team anyone can reuse the code and edit it but please till me through      |
   |                             the github page at this url                                    |
   |                (https://github.com/EveryTGames/Contacts-Manager/pulls)                     |
   |                        to learn from your edits, and sorry                                 |
   |                    for the spelling mistakes in the program                                |
   |                            im too lazy to recheck it :)                                    |
   |                                    *********                                               |
   |                                this code is from                                           |
   |                   (https://github.com/EveryTGames/Contacts-Manager)                        |
   |                                                                                            |
   |                                                                                            |
 * |____________________________________________________________________________________________|
 */

namespace cat_task2
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

                Console.WriteLine("no save file found,creating a new save");
                contacts = new List<Contact>();
            }
            pages.show_all_contacts_page();

            Console.ReadKey();

        }
    }
    [Serializable]
    class contact_manager
    {

        public static void save(List<Contact> _contacts)
        {
            contact_manager obj = new contact_manager();
            IFormatter formatter = new BinaryFormatter();
            obj.contacts = _contacts;
            Stream stream = new FileStream(@"\saved_contacts.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
        public static List<Contact> load()
        {

            Stream stream = new FileStream(@"\saved_contacts.txt", FileMode.Open, FileAccess.Read);
            IFormatter formatter = new BinaryFormatter();

            contact_manager obj = (contact_manager)formatter.Deserialize(stream);
            stream.Close();
            return obj.contacts;
        }
        public List<Contact> contacts;

    }
    [Serializable]
    class Contact
    {







        public int count { get { return users.Count; } }
        public List<user> users = new List<user>();
        public void addUser()
        { users.Add(new user()); }
        public void removeUser(int index, [Optional] user _user)
        {
            Console.WriteLine(users[index].city);
            if (_user == null)
                users.RemoveAt(index);
            else
                users.Remove(_user);
        }
        public void editUser(string property_name, int index_of_the_desired_data_type, string what_want_to_edit_in_this_data_type, string value, [Optional] user _user, int index = 0)
        {
            if (_user == null)
            {
                users[index].edit(property_name, value, index_of_the_desired_data_type, what_want_to_edit_in_this_data_type);
            }
            else
            {
                _user.edit(property_name, value, index_of_the_desired_data_type, what_want_to_edit_in_this_data_type);

            }

        }
        public void showAll()
        {
            foreach (user _user in users)
            {
                Console.WriteLine(_user);
            }
        }
        /// <summary>
        /// searchs in all the the users for a value 
        /// </summary>
        /// <param name="value"></param>
        /// <returns> return a list with the matched users as "user" type, and returns null if not found</returns>
        public List<user> searchInAllUsers(string value)
        {
            List<user> temp = new List<user>();
            foreach (user _user in users)
            {
                if (_user.searchInUser(value) == 1)
                {
                    temp.Add(_user);
                }
            }
            if (temp.Count > 0)
            {

                return temp;
            }
            else
            {
                return null;
            }
        }
        public Contact(int _NumberOfUsers = 0)//adds the users at one go if entered this parameter
        {
            for (int i = 0; i < _NumberOfUsers; i++)
            {

                users.Add(new user());

            }

        }
    }
    [Serializable]
    class user : Contact
    {
        /// <summary>
        /// it maps the data types so it can be passed to the input manager so it know what its type 
        /// -1 for options (instant return)
        ///  0  for integer input only
        /// 1 for date input only
        ///2 for strings that doesnt allow space in it
        ///3 for tables
        ///4 for general strings
        ///5 for addedDate
        /// </summary>
        public static Dictionary<string, int> categories = new Dictionary<string, int> {
             { "id",0 },
             { "firstname",2},
             { "lastname",2 },
             { "gender",2 }, {"city",2 }, { "birthday",1},{"addedDate",5 }, {"phone",0 },{ "email",2 },{ "adresse",4 },{ "type",4},{ "description",4}
        };
        //the property with name that ends with "s" must be array (for me if u want to use this :) ) 
        public int id { set; get; }
        public string firstname { set; get; }
        public string lastname { set; get; }
        public string gender { set; get; }
        public string city { set; get; }
        public string addedDate { get; set; }
        public string birthday { get; set; }


        public List<Phone> phones { set; get; } = new List<Phone>();
        public List<Email> emails { set; get; } = new List<Email>();
        public List<Adress> adresses { set; get; } = new List<Adress>();

        public override string ToString()
        {

            Console.WriteLine($"------------------data of user with id: {this.id}------------------------ ");
            Console.WriteLine("--------------------------------------------------");


            foreach (string variable in allVariables)
            {
                if (variable.EndsWith("s"))
                {


                    Console.WriteLine(string.Concat(this[variable, typeof(user)].result));



                }
                else
                    Console.Write($"{variable}: {this[variable, typeof(user)].result} \t");

                //Console.WriteLine();

            }
            Console.WriteLine("--------------------------------------------------------------------");
            return null;
        }

        public int searchInUser(string value)
        {



            foreach (string variable in allVariables)
            {
                if (variable.EndsWith("s"))
                {
                    for (int i = 0; i < objectDictionary[variable].Count; i++)
                    {
                        string temp = variable.Substring(0, variable.Length - 1);//temporary string to make the name of the property
                        PropertyInfo x = typeof(user).GetProperty(variable); //gets the list (i.e List<phone>) property in user class
                        Type c = x.PropertyType.GenericTypeArguments[0]; //gets the element type in that list 
                        string[] values = c.GetMethod("Get").Invoke(objectDictionary[variable][i], null); //invokes the Get method in the child types
                        foreach (string desired_value in values)
                        {
                            if (desired_value == value) { return 1; }
                        }


                    }
                }
                else
                {
                    PropertyInfo x = typeof(user).GetProperty(variable); //gets the property (i.e id,firstnname) property in user class

                    object q = x.GetValue(this); //  gets the value of the property
                    string temp = Convert.ToString(q);

                    if (temp == value)
                        return 1;

                }
            }
            return 0;



        }

        /// <summary>
        /// gets the "result" the value of the property if it wasnt a list, and gets the whole list or an element (by index) if it was  a list
        /// gets the "type" is the type of the elemnts that returned
        /// </summary>
        /// <param name="name_of_property"> it can be "id", "firstname", "lastname", "gender", "city", "addedDate", "phones", "emails" or "adresses"</param>
        /// <param name="_type"></param>
        /// <param name="index"></param>
        /// 
        /// <returns></returns>
        public (dynamic result, Type type ) this[string name_of_property, Type _type, int index = -1] //it works as the edit function too
        {
            get
            {

                PropertyInfo property = _type.GetProperty(name_of_property);
                if (!name_of_property.EndsWith("s"))
                {

                    return (property.GetValue(this), property.PropertyType);
                }
                else
                {

                    //code to get the array or element from it
                    dynamic list = property.GetValue(this);
                    if (index == -1)
                        return (list, property.PropertyType.GetGenericArguments()[0]);// returns the type which is inside the list (List<Type>)
                    else
                        return (list[index], property.PropertyType.GetGenericArguments()[0]);
                }


            }

            set
            {

                PropertyInfo property = _type.GetProperty(name_of_property);

                if (!name_of_property.EndsWith("s"))
                {
                    property.SetValue(this, Convert.ChangeType(value.result, property.PropertyType));
                }
                else//if the property was an array
                {
                    Array array = (Array)property.GetValue(this);

                    if (index == -1)
                        array = value.result;

                    else
                        array.SetValue(value.result, index);

                }
            }

        }
        public static string[] allVariables = { "id", "firstname", "lastname", "gender", "city", "addedDate", "birthday", "phones", "emails", "adresses" };
        public user(int x)//just to prevent declaring child instance from making new user (with values)
        {

        }


        /// <summary>
        /// a dictionary that holds the arrays (i.e phones,emails,adresses) 
        /// the string is the name of the array, the "List<dynamic>" is the array itself 
        /// </summary>
        Dictionary<string, List<dynamic>> objectDictionary = new Dictionary<string, List<dynamic>>();//here is the bug



        /// <summary>
        /// edits a specific value in the current user 
        /// </summary>
        /// <param name="property_name"> the property name should be "phones","emails" or "adresses"</param>
        /// <param name="indexe_to_edit">the orded of the desired object to be edited(it starts from 0)</param>
        /// <param name="value_to_edit">the name of the object that you want to edit
        /// ,(it can be "phone","email","adresse","type" or "description", and pay attention that some of them is not in all the data types 
        /// examble, phone variable is not in email data type)</param>
        /// <param name="value"> the desired value that u want to change to it</param>
        public void edit(string property_name, string value, int indexe_to_edit = 0, string value_to_edit = null)
        {
            if (property_name.EndsWith("s"))
            {
                object list;
                Type c;
                (list,c ) = this[property_name, typeof(user), indexe_to_edit];
                PropertyInfo f = c.GetProperty(value_to_edit);
                f.SetMethod.Invoke(list, new object[] { Convert.ChangeType(value, f.PropertyType) });
            }
            else
            {
                this[property_name, typeof(user)] = (value,null);


            }



        }
        /// <summary>
        /// adds specicif number of objects(emails,phones,adresses) to the current user
        /// ,the property name should be "phones","emails" or "adresses"
        /// </summary>
        /// <param name="property_name"> 
        ///  the property name should be "phones","emails" or "adresses"
        /// </param>
        /// <param name="number_of_things_to_add"> default is 1 </param>
        public void addTo(string property_name, int number_of_things_to_add = 1)

        {
            Type type = typeof(user);
            PropertyInfo property = type.GetProperty(property_name);
            Type G_type = property.PropertyType.GetGenericArguments()[0];
            List<object> temp = new List<object>();
            object list = property.GetValue(this);
            for (int i = 0; i < number_of_things_to_add; i++)
            {
                MethodInfo methodInfo = property.PropertyType.GetMethod("Add");
                temp.Add(Activator.CreateInstance(G_type, 1));


                methodInfo.Invoke(list, new object[] { temp.Last() });
            }
            if (objectDictionary.TryGetValue(property_name, out List<dynamic> _list))
            {

                _list.Add(temp);
            }
            else
            {
                objectDictionary.Add(property_name, temp);
            }




        }
        /// <summary>
        /// delets a set of objects (currrently just one at a time)in one data type like phones,emails or adresses,
        /// the property name should be "phones","emails" or "adresses"
        /// </summary>
        /// <param name="property_name">the property name should be "phones","emails" or "adresses"</param>
        /// <param name="indexes_to_delet">can be inserted as array or as parameters(the indexing starts with zero)</param>
        //        public void delet(string property_name, params int[] indexes_to_delet)
        //        {
        //            Type type = typeof(user);
        //            PropertyInfo property = type.GetProperty(property_name);
        //            List<object> temp = new List<object>();
        //            object list = property.GetValue(this);

        //            for (int i = 0; i < indexes_to_delet.Length; i++)//invokes the "RemoveAt" function in List<> dynamiclly
        //            {
        //                MethodInfo methodInfo = property.PropertyType.GetMethod("RemoveAt");
        //                // from here
        //                Console.WriteLine("-----------------------");
        //                Console.WriteLine(indexes_to_delet[i]);
        //                Console.WriteLine(objectDictionary[property_name].Count);
        ////to here is a bug tests not part of the program
        //                objectDictionary[property_name].RemoveAt(indexes_to_delet[i]);
        //                methodInfo.Invoke(list, new object[] { indexes_to_delet[i] });
        //                //the problem is that after u delet the indexes changes
        //and i will just remove this ability for now just to have a finished program then fix it later


        //            }
        // -----------------------------------------------------------------------------
        public void delet(string property_name,int index_to_delet)
        {
            Type type = typeof(user);
            PropertyInfo property = type.GetProperty(property_name);
            List<object> temp = new List<object>();
            object list = property.GetValue(this);

           
                MethodInfo methodInfo = property.PropertyType.GetMethod("RemoveAt");
             
                objectDictionary[property_name].RemoveAt(index_to_delet);
                methodInfo.Invoke(list, new object[] { index_to_delet });
              
               
                    



        }

    protected void data_type_constructor(string[] allVariables, Type type)//for declaring the child data types
        {
            foreach (string variable in allVariables)
            {
                Console.Write($"{variable}: ");
                string temp = Input.handle_input(user.categories[variable], false);
                this[variable, type] = (temp,null);
                Console.WriteLine();


            }

        }
        public user()
        {


            foreach (string variable in allVariables)
            {
                Console.Write($"{variable}: ");
                if (!variable.EndsWith("s"))
                {

                    string temp = Input.handle_input(categories[variable], false);
                    this[variable, typeof(user)] = (temp,null);
                    Console.WriteLine();
                }
                else
                {
                    Console.WriteLine();

                    Type type = typeof(user);
                    PropertyInfo property = type.GetProperty(variable);
                    object list = property.GetValue(this);
                    if (property == null)
                        Console.Write("no property with this name at this class");
                    else
                    {
                        Console.WriteLine($"enter the number of {variable}");
                        int size = int.Parse(Input.handle_input(0, false));

                        this.addTo(variable, size);




                    }


                }

            }

        }

    }
    [Serializable]
    class Phone : user
    {

        public string phone { set; get; }
        public string type { set; get; }
        public string description { set; get; }


        new string[] allVariables = { "phone", "type", "description" };

        public string[] Get()
        {
            return new string[] { phone, type, description };

        }
        public void setPhone(string _phone)
        { this.phone = _phone; }
        public void setType(string _type)
        { type = _type; }
        public void setDescription(string _dis)
        { description = _dis; }

        public Phone(int x) : base(x)
        {
            data_type_constructor(allVariables, typeof(Phone));


        }
        public override string ToString()
        {
            Console.WriteLine($"\n phone : {phone} \t type : {type} \t description : {description} \n");

            return null;
        }
    }
    [Serializable]
    class Email : user
    {
        public string email { set; get; }
        public string type { set; get; }
        public string description { set; get; }
        new string[] allVariables = { "email", "type", "description" };

        public string[] Get()
        {
            return new string[] { email, type, description };

        }
        public void setPhone(string _email)
        { this.email = _email; }
        public void setType(string _type)
        { type = _type; }
        public void setDescription(string _dis)
        { description = _dis; }
        public Email(int x) : base(x)
        {
            data_type_constructor(allVariables, typeof(Email));


        }
        public override string ToString()
        {
            Console.WriteLine($"email : {email} \t type : {type} \t description : {description} \n");

            return null;
        }

    }
    [Serializable]
    class Adress : user
    {
        public string adresse { set; get; } // i know that it is without "e" but my logic depands on the name :)
        public string type { set; get; }
        public string description { set; get; }
        new string[] allVariables = { "adresse", "type", "description" };


        public string[] Get()
        {
            return new string[] { adresse, type, description };

        }
        public void setPhone(string _adresse)
        { this.adresse = _adresse; }
        public void setType(string _type)
        { type = _type; }
        public void setDescription(string _dis)
        { description = _dis; }



        public Adress(int x) : base(x)
        {

            data_type_constructor(allVariables, typeof(Adress));


        }
        public override string ToString()
        {
            Console.WriteLine($"adress : {adresse} \t type : {type} \t description : {description}  \n");

            return null;
        }

    }






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
            Console.WriteLine("[1]open contact    [2]add contact      [3]delet contact  [4]search in all contacts      [5]quit the program   ");
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
                    Console.Write("enter the index of the contact you want to delet (it starts from 0)");
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
            Console.WriteLine("[1]select user to edit    [2]add user      [3]delet user   [4]search in this contact  [5]go back");
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
                    Console.Write("enter the index of the user you want to delet (it starts from 0)");
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


            _user_.ToString();
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
            Console.WriteLine("[1]edit a data type   [2]add a data type    [3]delet a data type   [Esc]cancel");
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
                    if (ans >= x.Count )
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
                        _user.editUser(selected_category, index_of_the_desired_data_type, what_want_to_edit_in_this_data_type, value,_user_);
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
                    //this is for the multy deletion abitlity but found aproblem and mentioned it in a comment in the "delet" function
                    //    Console.WriteLine("enter the number of things u want to delet");
                    //    int n = int.Parse(option(0, false, true));
                    //cc: Console.WriteLine("enter the indexes of things u want to delet(it starts from zero and press enter after each index)");


                    //    int[] n_removed_obj = new int[n];
                    //    for (int i = 0; i < n; i++)
                    //    {
                    //     n_removed_obj[i] = int.Parse(option(0, false, true));
                    //        if (n_removed_obj[i] >= _user[selected_category, typeof(user)].result.Count)
                    //        {
                    //            Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0,enter the indexes again");
                    //            goto cc;
                    //        }
                    //    }
                    //    _user.delet(selected_category, n_removed_obj);
                    //    show_all_user_data_page(ref _contact, ref _user_);
                    //    break;
                  
                cc: Console.WriteLine("enter the index of what u want to delet(it starts from 0)");


                    int index_removed_obj ;
                    
                        index_removed_obj = int.Parse(option(0, false, true));
                        if (index_removed_obj >= _user[selected_category, typeof(user)].result.Count)
                        {
                            Console.WriteLine("not found, please remember to creat one if needed and the index starts from 0,enter the index again");
                            goto cc;
                        }
                    
                    _user.delet(selected_category, index_removed_obj);
                    show_all_user_data_page(ref _contact, ref _user_);
                    break;


                default: goto aa;

            }






        }
        public static void search_page(Contact _contact = null)
        {
            Console.Clear();

            coloring();

            Console.WriteLine("----------------------search page--------------------------");
            Console.WriteLine("[0]search again      [1]open a contact      [2]open user        [Esc]cancel");
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
                    }
                    else
                    {

                        Console.WriteLine($"({temp.Count}) users matched in it");

                        _contacts.Add(temp); //saves the list of the users in the corresponding index (with the contact)
                        Console.WriteLine("-----------------------------------------------");
                        foreach (user _user in temp)
                        {
                            _user.ToString();

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
                    { _user.ToString(); }
            }





        aa: int ans = int.Parse(option(-1, true, true));
            switch (ans)
            {
                case 0:
                    goto dd;

                case 1:
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
                    Console.Write("enter the index for the contact then press enter then the user you want to open (it starts from 0 for each contact)");
                    int x = int.Parse(option(0, false, true));
                    if (x >= _contacts.Count)
                    {
                        Console.WriteLine("not found, please remember the index starts from 0, choose one of the options again");
                        goto aa;
                    }
                    else
                    {
                        Console.WriteLine("enter the index for the user you want to open in this contact");
                    bb: ans = int.Parse(option(0, false, true));
                        if (ans >= _contacts[x].Count)
                        {
                            Console.WriteLine("not found, please remember the index starts from 0, try again");
                            goto bb;
                        }
                        Contact contact = Program.contacts[x];
                        user _user = _contacts[x][ans];
                        show_all_user_data_page(ref contact, ref _user);
                    }

                    break;

                default:
                    goto aa;
            }




        }







    }















}
///for handling the input
namespace input
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




        public static void whiteSpaces()
        {
            for (int i = 0; i < Console.WindowHeight; i++)
            {
                for (int j = 0; j < Console.WindowWidth; j++)
                {

                    Input.wsc(" ", i, j);
                }
                Console.WriteLine();
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
        


        public static void deletelast()
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
            bool deletable;
            while (on)
            {
                if(input.Length >= 9 && (type == 0 || type == 3))
                {
                    on = false;
                    temp = input.ToString().Substring(0,9);
                    input.Clear();
                    return temp;
                    
                }
                (int x, int y) = (Console.CursorLeft, Console.CursorTop);

                key = Console.ReadKey();

                if ((char.IsLetterOrDigit(key.KeyChar) || char.IsSymbol(key.KeyChar)) || char.IsPunctuation(key.KeyChar) || char.IsSeparator(key.KeyChar))
                    deletable = true;
                else
                    deletable = false;

                if ((key.Key == ConsoleKey.Escape) && escapable)
                {
                    return null;



                }
                if (deletable && hidden)
                    deletelast();

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
                            deletelast();

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
                            deletelast();
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
                                            deletelast();
                                        Console.SetCursorPosition(x, y);
                                        break;
                                    }
                                }
                                else
                                {
                                    if (!hidden)
                                        deletelast();
                                    Console.SetCursorPosition(x, y);
                                    break;
                                }
                                input.Append(key.KeyChar);
                                break;

                            }
                            if (!hidden && deletable)
                                deletelast();
                            Console.Beep();

                            break;

                        case -1:
                        case 3:
                        case 0:
                            if (deletable && !hidden)
                                deletelast();
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