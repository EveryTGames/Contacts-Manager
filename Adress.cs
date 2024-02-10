using System;


namespace cat_task2_final
{
    [Serializable]
    class Adress : user
    {
        public string adresse { set; get; } // i know that it is without "e" but my logic depands on the name :)
        public string type { set; get; }
        public string description { set; get; }

        user parent;
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



        public Adress(user _parent,int x) : base(x)
        {
            parent = _parent;

            data_type_constructor(allVariables, typeof(Adress));


        }
        public override string ToString()
        {
            Console.WriteLine($" ({parent.objectDictionary["adresses"].IndexOf(this)}) adress : {adresse} \t type : {type} \t description : {description}  \n");

            return null;
        }

    }

}
