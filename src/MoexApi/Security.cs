using System;
using System.Collections.Generic;

namespace MoexXL.MoexApi
{
    public class Security
    {
        public Dictionary<string, object> Attributes { get; private set; }

        public Security()
        {
            Attributes = new Dictionary<string, object>();
        }

        public void AddAttribute(string key, object value)
        {
            if (DateTime.TryParse(value as string, out DateTime dt))
                value = dt;

            if (!Attributes.ContainsKey(key))
                Attributes.Add(key, value);
        }
    }
}
