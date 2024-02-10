using System;


namespace cat_task2_final
{
    [Serializable]
    class Phone : user
    {

        public string phone { set; get; }
        public string type { set; get; }
        public string description { set; get; }
        user parent;

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

        public Phone(user _parent,int x) : base(x)
        {
            parent = _parent;
            data_type_constructor(allVariables, typeof(Phone));


        }
        public override string ToString()
        {
            Console.WriteLine($"\n ({parent.objectDictionary["phones"].IndexOf(this)}) phone : {phone} \t type : {type} \t description : {description} \n");

            return null;
        }
    }
}
