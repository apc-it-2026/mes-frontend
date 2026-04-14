using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class FunButtonClass
    {
        public string Text;
        public string Action;
        public string Url;
        public string DllName;
        public string ClassName;
        public string Method;
        public Dictionary<string,string> Parameters;

        public FunButtonClass(string XML)
        {
            Parameters = new Dictionary<string, string>();
            this.Text = Common.StringHelper.GetDataFromFirstTag(XML, "<Text>", "</Text>").Trim();
            this.Action = Common.StringHelper.GetDataFromFirstTag(XML, "<Action>", "</Action").Trim();
            this.Url = Common.StringHelper.GetDataFromFirstTag(XML, "<URL>", "</URL>").Trim();
            try
            {
                this.DllName = Common.StringHelper.GetDataFromFirstTag(XML, "<DllName>", "</DllName>").Trim();
                this.ClassName = Common.StringHelper.GetDataFromFirstTag(XML, "<ClassName>", "</ClassName>").Trim();
                this.Method = Common.StringHelper.GetDataFromFirstTag(XML, "<Method>", "</Method>").Trim();
            }
            catch { }

            string paStr = Common.StringHelper.GetDataFromFirstTag(XML, "<Parameters>", "</Parameters>").Trim();
            
            if(!string.IsNullOrEmpty(paStr))
            {
                string[] s = new string[1];
                s[0] = "@;";
                string[] ss = paStr.Split(s, StringSplitOptions.RemoveEmptyEntries);
                s[0] = "@:";
                foreach(string s2 in ss)
                {
                    Parameters.Add(s2.Split(s, StringSplitOptions.None)[0], s2.Split(s, StringSplitOptions.None)[1]);
                }
            }

        }
    }
}
