using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace cat_task2_final
{
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
        //dont forget to add corresponding handle input type in "categories dictionery above,if u want to add a new property, amd also add it in "allvariables" array, 
        //categories dictionery has a mapping for each property name even in subclasses as "email"
        //allvariables array has the names of the properties in user class only
        //and also each data type class has allvariables array which has the names of the properities in that class wich will be passed to the data_type_constructor function
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

        public  string ToString(int index)
        {
            
            Console.WriteLine($"------------------data of user (index: {index}) with id : {this.id}------------------------ ");
            Console.WriteLine("--------------------------------------------------");


            foreach (string variable in allVariables)
            {
                if (variable.EndsWith("s"))
                {


                    Console.WriteLine(string.Concat(this[variable, typeof(user)].result));



                }
                else
                    Console.Write($"{variable}: {this[variable, typeof(user)].result} \t");

              

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
        public (dynamic result, Type type) this[string name_of_property, Type _type, int index = -1] //it works as the edit function too
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
       public Dictionary<string, List<dynamic>> objectDictionary = new Dictionary<string, List<dynamic>>();



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
                (list, c) = this[property_name, typeof(user), indexe_to_edit];
                PropertyInfo f = c.GetProperty(value_to_edit);
                f.SetMethod.Invoke(list, new object[] { Convert.ChangeType(value, f.PropertyType) });
            }
            else
            {
                this[property_name, typeof(user)] = (value, null);


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
                temp.Add(Activator.CreateInstance(G_type,new object[] { this, 1 }));


                methodInfo.Invoke(list, new object[] { temp.Last() });
            }
            if (objectDictionary.TryGetValue(property_name, out List<dynamic> _list))
            {

                _list.AddRange(temp);
            }
            else
            {
                objectDictionary.Add(property_name, temp);
            }




        }
        /// <summary>
        /// deletes a set of objects (currrently just one at a time)in one data type like phones,emails or adresses,
        /// the property name should be "phones","emails" or "adresses"
        /// </summary>
        /// <param name="property_name">the property name should be "phones","emails" or "adresses"</param>
        /// <param name="indexes_to_delete">can be inserted as array or as parameters(the indexing starts with zero)</param>
        public void delete(string property_name, params int[] indexes_to_delete)
        {
            Type type = typeof(user);
            PropertyInfo property = type.GetProperty(property_name);
            List<object> temp = new List<object>();
            object list = property.GetValue(this);
           indexes_to_delete = indexes_to_delete.OrderByDescending(item => item).ToArray();

            for (int i = 0; i < indexes_to_delete.Length; i++)//invokes the "RemoveAt" function in List<> dynamiclly
            {
                MethodInfo methodInfo = property.PropertyType.GetMethod("RemoveAt");

                objectDictionary[property_name].RemoveAt(indexes_to_delete[i]);
                methodInfo.Invoke(list, new object[] { indexes_to_delete[i] });

            }
        }
     

            protected void data_type_constructor(string[] allVariables, Type type)//for declaring the child data types
        {
            foreach (string variable in allVariables)
            {
                Console.WriteLine();
                Console.Write($"{variable}: ");
                string temp = Input.handle_input(user.categories[variable], false);
                this[variable, type] = (temp, null);
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
                    Console.WriteLine();

                    string temp = Input.handle_input(categories[variable], false);
                    this[variable, typeof(user)] = (temp, null);
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
}
