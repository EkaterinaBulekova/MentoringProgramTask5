using System.Linq;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ReflectionHelperLibrary;

namespace ReflectionHelperTests
{
    [TestClass]
    public class ReflectionHelperTests
    {
        [TestMethod]
        public void GetPropertyValueSimplePropertyName()
        {
            var obj = new MyClass();
            var name = ReflectionHelper.GetPropertyValue(obj, "Name");
            Assert.AreEqual(name, "Andrew");
        }

        [TestMethod]
        public void GetPropertyValueComplexPropertyName()
        {
            var obj = new MyClass();
            var adress = ReflectionHelper.GetPropertyValue(obj, "Adress.City");
            Assert.AreEqual(adress, "London");
        }

        [TestMethod]
        public void HasPropertyWhenPropertyExists()
        {
            var obj = new MyClass();
            var hasProp = ReflectionHelper.HasProperty(obj, "Adress.City");
            Assert.IsTrue(hasProp);
        }

        [TestMethod]
        public void HasPropertyWhenPropertyNotExists()
        {
            var obj = new MyClass();
            var hasProp = ReflectionHelper.HasProperty(obj, "Adress.Street");
            Assert.IsFalse(hasProp);
        }

        [TestMethod]
        public void GetPropertyValueForListofProperties()
        {
            var obj = new MyClass();
            var name = ReflectionHelper.GetPropertyValue(obj, "Adress.City", "Adress.PostCod");
            Assert.AreEqual(name, "London");
        }

        [TestMethod]
        public void GetPropertyCanGetExpectedPropertyFromObject()
        {
            var obj = new MyClass();
            var property = ReflectionHelper.GetProperty(obj, "Adress.City");
            Assert.IsInstanceOfType(property, typeof(PropertyInfo));
            Assert.AreEqual(property.Name, "City");
            Assert.AreEqual(property.PropertyType, typeof(string));
        }

        [TestMethod]
        public void GetPropertyCanGetExpectedPropertyFromType()
        {
            var property = ReflectionHelper.GetProperty(typeof(MyClass), "Adress.City");
            Assert.IsInstanceOfType(property, typeof(PropertyInfo));
            Assert.AreEqual(property.Name, "City");
            Assert.AreEqual(property.PropertyType, typeof(string));
        }

        [TestMethod]
        public void CanGetPropertyType()
        {
            var obj = new MyClass();
            var type = ReflectionHelper.GetPropertyType(obj, "Adress.PostCod");
            Assert.AreEqual("Int32", type.Name);
        }

        [TestMethod]
        public void CanGetCustomAttribute()
        {
            var obj = new MyClass();
            var attributesrs = ReflectionHelper.GetCustomAttributes(obj.GetType().GetProperty("Adress")).ToList();
            Assert.AreEqual(2, attributesrs.Count);
            Assert.IsInstanceOfType(attributesrs[0], typeof(MyAttribute));
            Assert.IsInstanceOfType(attributesrs[1], typeof(YouAttribute));
        }


        [TestMethod]
        public void CanGetMethodsInfo()
        {
            var obj = new MyClass();
            var methods = ReflectionHelper.GetMethodsInfo(obj).Where(_ => _.Name.Contains("Method")).ToList();
            Assert.AreEqual(2, methods.Count);
            Assert.IsInstanceOfType(methods[0], typeof(MethodInfo));
            Assert.IsInstanceOfType(methods[1], typeof(MethodInfo));
            Assert.AreEqual(typeof(int), methods[0].ReturnType);
            Assert.AreEqual(typeof(string), methods[1].ReturnType);
            Assert.AreEqual("MyMethod", methods[0].Name);
            Assert.AreEqual("MySecondMethod", methods[1].Name);
        }


        [TestMethod]
        public void CanGetFieldsInfo()
        {
            var obj = new MyClass();
            var fields = ReflectionHelper.GetFieldsInfo(obj);
            Assert.AreEqual(4, fields.Count);
            Assert.IsInstanceOfType(fields[0], typeof(FieldInfo));
            Assert.AreEqual(typeof(string), fields[0].FieldType);
            Assert.AreEqual(typeof(MyAdress), fields[1].FieldType);
            Assert.AreEqual("_stringField", fields[0].Name);
            Assert.AreEqual("_adressField", fields[1].Name);
        }

        [TestMethod]
        public void CanCallMethod()
        {
            var obj = new MyClass();
            var myString = "some string";
            var result1 = ReflectionHelper.CallMethod(obj, "MyMethod", null);
            var result2 = ReflectionHelper.CallMethod(obj, "MySecondMethod", new[] {myString});
            Assert.AreEqual(2, (int)result1);
            Assert.AreEqual(myString, (string)result2);
        }
    }
}
