using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Web
{
    public class JSONFormClass
    {
        public static string moduleNoEdit = "生产工单-产品条码,生产工单-BOM资料,生产工单-转移记录,生产工单-投料记录,生产工单-不良记录,生产工单-报工资料,生产工单-工序数据采集明细";

        public string APPCode=string.Empty;
        public string APPName=string.Empty;
        public JSONPanelClassH PanelH;
        public List<JSONPanelClassB> PanelB;
        public JSONPanelClassHList PanelHList;
  

        public JSONFormClass()
        {

        }

        public static bool GetIsNULL(SJeMES_Framework.Web.JSONControlH H)
        {
            bool ret = false;
            foreach(JSONControlHRules jhr in H.rules)
            {
                if(jhr.type == "required")
                {
                    return true;
                }
             
            }

            return ret;
        }

        public static string GetDataType(SJeMES_Framework.Web.JSONControlH H)
        {
            string ret = "String";
            switch (H.type)
            {
                case "text":
                    foreach (JSONControlHRules jhr in H.rules)
                    {
                        if (jhr.type == "digits")
                        {
                            return "Int";
                        }

                        if (jhr.type == "number")
                        {
                            return "Float";
                        }

                    }
                    break;
                case "switch":
                        return "Bool";
                    break;
                case "datePicker":
                    if(H.control.format== "ymd")
                    {
                        return "Date";
                    }
                    else if(H.control.format == "ymdHms")
                    {
                        return "DateTime";
                    }
                    else if (H.control.format == "Hms")
                    {
                        return "Time";
                    }
                    break;
                case "select":
                    return "Enum";
                    break;
                case "other":
                    return "DataSource";
                    break;
            }



            return ret;
        }

        public static string GetDataType(SJeMES_Framework.Web.JSONControlB B)
        {
            string ret = "String";
            switch (B.type)
            {
                case "input":
                    foreach (JSONControlHRules jhr in B.rules)
                    {
                        if (jhr.type == "digits")
                        {
                            return "Int";
                        }

                        if (jhr.type == "number")
                        {
                            return "Float";
                        }

                    }
                    break;
                case "switch":
                    return "Bool";
                    break;
                case "datePicker":
                    if (B.format == "ymd")
                    {
                        return "Date";
                    }
                    else if (B.format == "ymdHms")
                    {
                        return "DateTime";
                    }
                    else if (B.format == "Hms")
                    {
                        return "Time";
                    }
                    break;
                case "select":
                    return "Enum";
                    break;
                case "other":
                    return "DataSource";
                    break;
            }



            return ret;
        }

        public static string GetEnumData(SJeMES_Framework.Web.JSONControlH H)
        {
            string ret = string.Empty;

            foreach(SJeMES_Framework.Web.JSONEnum e in H.enumData)
            {
                ret += e.label + "@:" + e.value + "@;";
            }

            return ret;
        }

        public static string GetEnumData(SJeMES_Framework.Web.JSONControlB B)
        {
            string ret = string.Empty;

            foreach (SJeMES_Framework.Web.JSONEnum e in B.enumData)
            {
                ret += e.label + "@:" + e.value + "@;";
            }

            return ret;
        }

        public static SJeMES_Framework.Web.JSONControlH GetHTypeAndRules(SJeMES_Framework.Web.JSONControlH jch, string DataType)
        {

            switch (DataType)
            {
                //text(文本输入框),password(密码输入框),radio(单选),checkbox(多tfp选框),datePicker(日期选择),seelct(下拉选择),txt(纯文本)
                case "String":
                    jch.type = "text";
                    break;
                case "Int":
                    jch.type = "text";
                    jch.rules.Add(new SJeMES_Framework.Web.JSONControlHRules("digits", "只能输入正整数"));
                    break;
                case "Float":
                    jch.type = "text";
                    jch.rules.Add(new SJeMES_Framework.Web.JSONControlHRules("number", "只能输入数字"));
                    break;
                case "Bool":
                    jch.type = "switch";
                    break;
                case "Date":
                    jch.type = "datePicker";
                    jch.control.type = "ymd";
                    jch.control.format = "ymd";
                    break;
                case "DateTime":
                    jch.type = "datePicker";
                    jch.control.type = "ymdHms";
                    jch.control.format = "ymdHms";
                    break;
                case "Time":
                    jch.type = "datePicker";
                    jch.control.type = "Hms";
                    jch.control.format = "Hms";
                    break;
                case "Enum":
                    try
                    {
                        jch.type = "select";
                    }
                    catch { }
                    break;
                case "DataSource":
                    jch.type = "other";

                    break;
                case "OtherData":
                    jch.type = "text";
                    jch.control.IsAdd = false;
                    jch.control.IsEdit = false;

                    break;
                default:
                    jch.type = "text";
                    break;
            }
            return jch;
        }

        public static SJeMES_Framework.Web.JSONControlB GetBTypeAndRules(SJeMES_Framework.Web.JSONControlB jcb, string DataType)
        {

            switch (DataType)
            {

                case "String":
                    jcb.type = "input";
                    break;
                case "Bool":
                    jcb.type = "switch";
                    break;
                case "Date":
                    jcb.type = "date";
                    jcb.format = "ymd";
                    jcb.datatype = "ymd";
                    break;
                case "DateTime":
                    jcb.type = "date";
                    jcb.format = "ymdHms";
                    jcb.datatype = "ymdHms";
                    break;
                case "Time":
                    jcb.type = "date";
                    jcb.format = "Hms";
                    jcb.datatype = "Hms";
                    break;
                case "Enum":
                    try
                    {
                        jcb.type = "select";
                       
                    }
                    catch { }
                    break;
                case "DataSource":
                    jcb.type = "other";
                   
                    break;
                case "OtherData":
                    jcb.type = "txt";
                    jcb.disabled = true;

                    break;
                default:
                    jcb.type = "input";
                    break;
            }
            return jcb;
        }

    }
}
