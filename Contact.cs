using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;


namespace cat_task2_final
{
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
                Console.WriteLine(_user.ToString(users.IndexOf(_user)));
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
}
