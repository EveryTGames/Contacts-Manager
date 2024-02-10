using System;


namespace cat_task2_final
{
    [Serializable]
    class Email : user
    {
        public string email { set; get; }
        public string type { set; get; }
        public string description { set; get; }

        user parent;
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
        public Email(user _parent,int x) : base(x)
        {
            parent = _parent;
            data_type_constructor(allVariables, typeof(Email));


        }
        public override string ToString()
        {
            Console.WriteLine($" ({parent.objectDictionary["emails"].IndexOf(this)}) email : {email} \t type : {type} \t description : {description} \n");

            return null;
        }

    }
}
