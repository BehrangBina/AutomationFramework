using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace AutomationFramework.Util.Behrang.ReflectionHandler
{
    /// <summary>
    /// create dynamic object and dynamic properties
    /// </summary>
    class ObjectManipulator
    {
        public static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
        /// <summary>
        /// get the property name of an object in order of appearance
        /// </summary>
        /// <param name="className">Class Name</param>
        /// <returns></returns>
        public static List<string> GetObjectPropertyNames(object className)
        {
            var fieldProperties = className.GetType()
                .GetProperties()
                .Select(field => field.Name)
                .ToList();
            return fieldProperties;
        }
        /// <summary>
        /// get the value of a property in order of appearance
        /// </summary>
        /// <param name="className">Class name</param>
        /// <returns></returns>
        public static List<object> GetObjectPropertyValues(object className)
        {
            var fieldValues = className.GetType()
                .GetProperties()
                .Select(field => field.GetValue(className))
                .ToList();
            return fieldValues;
        }
    }
}
