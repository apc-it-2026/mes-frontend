using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.Common
{
    class Internationalization
    {

        Dictionary<string, string> Data;

        public Internationalization()
        {
            Data = new Dictionary<string, string>();
        }

        public string GetValue (string Language)
        {
            string ret = string.Empty;

            if(Data.ContainsKey(Language))
            {
                ret = Data[Language];
            }

            return ret;

        }

        public void SetValue(string Language, string Value)
        {


            if (Data.ContainsKey(Language))
            {
                Data[Language] = Value;
            }
            else
            {
                Data.Add(Language, Value);
            }

        }
    }
}
