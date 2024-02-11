using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;


namespace cat_task2_final
{
    [Serializable]
    class contact_manager
    {

        public static void save(List<Contact> _contacts)
        {
            contact_manager obj = new contact_manager();
            IFormatter formatter = new BinaryFormatter();
            obj.contacts = _contacts;
            Stream stream = new FileStream(@"saved_contacts.txt", FileMode.Create, FileAccess.Write);
            formatter.Serialize(stream, obj);
            stream.Close();
        }
        public static List<Contact> load()
        {

            Stream stream = new FileStream(@"saved_contacts.txt", FileMode.Open, FileAccess.Read);
            IFormatter formatter = new BinaryFormatter();

            contact_manager obj = (contact_manager)formatter.Deserialize(stream);
            stream.Close();
            return obj.contacts;
        }
        public List<Contact> contacts;

    }
}
