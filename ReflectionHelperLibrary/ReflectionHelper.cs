using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;

namespace ReflectionHelperLibrary
{
	public static class ReflectionHelper
	{
		public static object GetPropertyValue(object instance, string propertyName)
		{
            //желательно предусмотреть чтобы propertyName мог быть составным(например address.city.region)
            if (!propertyName.Contains('.')) return instance.GetType().GetProperty(propertyName)?.GetValue(instance);
            var index = propertyName.IndexOf('.');
            return GetPropertyValue(
                instance.GetType().GetProperty(propertyName.Substring(0, index))?.GetValue(instance),
                propertyName.Substring(index + 1));
        }

		public static object GetPropertyValueByType(object instance, string propertyType)
		{
            //вернуть первый из найденых property с заданными типом.
		    return instance.GetType().GetProperties().FirstOrDefault(_ => _.PropertyType.Name == propertyType);
		}

		public static bool HasProperty(object instance, string propertyName)
		{
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
		    return GetProperty(instance, propertyName) != null;
		}

        public static object GetPropertyValue(object instance, params string[] propertyNames)
		{
			//вернуть первый из найденых property с заданными именами.
		    return GetPropertyValue(instance, propertyNames.FirstOrDefault(_ => HasProperty(instance, _)));
		}

		public static PropertyInfo GetProperty(Type type, string propertyName)
		{
		    if (!propertyName.Contains('.')) return type.GetProperty(propertyName);
		    var index = propertyName.IndexOf('.');
		    return GetProperty(type.GetProperty(propertyName.Substring(0, index))?.PropertyType,
		        propertyName.Substring(index + 1));
		}

        public static PropertyInfo GetProperty(object instance, string propertyName)
		{
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
		    return GetProperty(instance.GetType(), propertyName);
        }

        public static Type GetPropertyType(object instance, string propertyName)
		{
            //желательно предусмотреть чтобы propertyName мог быть составным (например address.city.region)
		    return GetProperty(instance, propertyName).PropertyType;
		}

        public static List<Attribute> GetCustomAttributes(PropertyInfo property)
        {
            return property.GetCustomAttributes().ToList();
        }

		public static List<MethodInfo> GetMethodsInfo(object instance)
		{
			return instance.GetType().GetMethods().ToList();
		}

		public static List<FieldInfo> GetFieldsInfo(object instance)
		{
			return instance.GetType().GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public).ToList();
		}

		public static object CallMethod(object instance, string methodName, object[] param)
		{
		    return instance.GetType().GetMethod(methodName)?.Invoke(instance, param);
		}
	}
}
