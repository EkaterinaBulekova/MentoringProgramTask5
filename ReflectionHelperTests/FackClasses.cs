using System;

namespace ReflectionHelperTests
{
    class MyAdress
    {
        [My("Some name")]
        public string City { get; set; }

        [My("Another name")]
        public int PostCod { get; set; }
    }

    class MyClass
    {
        private string _stringField;
        private MyAdress _adressField;

        public MyClass()
        {
            Name = "Andrew";

            Adress = new MyAdress()
            {
                City = "London",
                PostCod = 121341
            };
        }

        [My("Some name")]
        public string Name { get; set; }

        [My("Another name")]
        [You("Your name")]
        public MyAdress Adress { get; set; }

        public int MyMethod()
        {
            return 2;
        }

        public string MySecondMethod(string str)
        {
            return str;
        }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class MyAttribute : Attribute
    {
        public MyAttribute(string name) => Name = name;
        public string Name { get; }
    }

    [AttributeUsage(AttributeTargets.All)]
    public class YouAttribute : Attribute
    {
        public YouAttribute(string name) => Name = name;
        public string Name { get; }
    }

}
