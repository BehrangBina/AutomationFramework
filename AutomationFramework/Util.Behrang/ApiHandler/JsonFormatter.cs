using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace AutomationFramework.Behrang.Util.ApiHandler
{
    /// <summary>
    /// Remove a Token from a JSON String
    /// </summary>
    public static class JsonFormatter
    {
        public static JToken RemoveFields(this JToken token, string[] fields)
        {
            JContainer container = token as JContainer;
            if (container == null) return token;

            List<JToken> removeList = new List<JToken>();
            foreach (JToken el in container.Children())
            {
                JProperty p = el as JProperty;
                if (p != null && fields.Contains(p.Name))
                {
                    removeList.Add(el);
                }
                el.RemoveFields(fields);
            }

            foreach (JToken el in removeList)
            {
                el.Remove();
            }

            return token;
        }
    }

}
