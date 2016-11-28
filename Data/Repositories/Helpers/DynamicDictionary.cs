using System.Collections.Generic;
using System.Reflection;

namespace Data.Repositories.Helpers
{
    public class DynamicDictionary : Dictionary<string, dynamic>
    {
        public DynamicDictionary(dynamic input)
        {
            var properties = input.GetType().GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                this.Add(prop.Name, prop.GetValue(input, null));
            }
        }

        private DynamicDictionary(){}
    }
}
