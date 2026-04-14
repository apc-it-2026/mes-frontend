using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormDesign.Control
{
    public class DataViewColumnClass :ControlClass
    {
        public string Title;
        public string DataType;
        public string DataKey;
        public bool Edit;
        public bool Add;
        public bool Enable;
        public string DataSelectSQL;
        public string DataShowSQL;
        public Dictionary<string, string> DataEnum;
        public string DateTimeFormat;
        public string DefaultValueType;
        public string DefaultValue;
        public List<string> Keys;
        public int Width;

        public bool IsNull;

        public DataViewColumnClass(Forms.FormClass FC, string XML)
        {
            try
            {
                this.FC = FC;
                Title = Common.StringHelper.GetDataFromFirstTag(XML, "<Title>", "</Title").Trim();
                DataType = Common.StringHelper.GetDataFromFirstTag(XML, "<DataType>", "</DataType>").Trim();
                DataKey = Common.StringHelper.GetDataFromFirstTag(XML, "<DataKey>", "</DataKey>").Trim();

                try
                {
                    Add = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Add>", "</Add>"));
                }
                catch { Add = true; }

            try
            {
                Edit = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Edit>", "</Edit>")); ;
            }
            catch { Edit = true; }


          
            try
            {
                Enable = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<Enable>", "</Enable>"));
            }
            catch { Enable = true; }


            DataSelectSQL = Common.StringHelper.GetDataFromFirstTag(XML, "<DataSelectSQL>", "</DataSelectSQL>").Trim();
                DataShowSQL = Common.StringHelper.GetDataFromFirstTag(XML, "<DataShowSQL>", "</DataShowSQL>").Trim();
                DateTimeFormat = Common.StringHelper.GetDataFromFirstTag(XML, "<DateTimeFormat>", "</DateTimeFormat>").Trim();
                DefaultValueType = Common.StringHelper.GetDataFromFirstTag(XML, "<DefaultValueType>", "</DefaultValueType>".Trim());
                DefaultValue = Common.StringHelper.GetDataFromFirstTag(XML, "<DefaultValue>", "</DefaultValue>").Trim();
                try
                {
                    Width = Convert.ToInt32(Common.StringHelper.GetDataFromFirstTag(XML, "<Width>", "</Width>").Trim());
                }
                catch {
                    Width = 200;
                }


                IsNull = true;

                if (XML.Contains("<IsNull>") && XML.Contains("</IsNull>"))
                {
                    if (!string.IsNullOrEmpty(Common.StringHelper.GetDataFromFirstTag(XML, "<IsNull>", "</IsNull>")))
                    {
                        IsNull = Convert.ToBoolean(Common.StringHelper.GetDataFromFirstTag(XML, "<IsNull>", "</IsNull>"));
                    }
                }

                DataEnum = new Dictionary<string, string>();
                string strDataEnum = Common.StringHelper.GetDataFromFirstTag(XML, "<DataEnum>", "</DataEnum").Trim();
                if (strDataEnum.IndexOf("@:") >-1&&strDataEnum.IndexOf("@;")>-1)
                {
                    string[] s = new string[1];
                    s[0] = "@;";

                    foreach (string sTmp in strDataEnum.Split(s,StringSplitOptions.RemoveEmptyEntries))
                    {
                        s[0] = "@:";
                        DataEnum.Add(sTmp.Split(s, StringSplitOptions.None)[0], sTmp.Split(s, StringSplitOptions.None)[1]);
                    }
                }


                Keys = new List<string>();

                string strTMP = Common.StringHelper.GetDataFromFirstTag(XML, "<Keys>", "</Keys>").Trim();
                if (strTMP.LastIndexOf(",") > -1)
                {
                    string[] s = strTMP.Split(',');
                    foreach (string ss in s)
                    {
                        Keys.Add(ss);
                    }
                }
                else if (!string.IsNullOrEmpty(strTMP))
                {
                    Keys.Add(strTMP);
                }
            }
            catch { }
        }
    }
}
