using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace SJeMES_Framework.Common
{
    public class JsonHelper
    {

        public static string GetJsonKeyValue(string Keys,string Values)
        {
            string ret = string.Empty;

            try
            {
                Keys = Keys.Replace("，", ",");
                Values = Values.Replace("，", ",");

                string[] key = Keys.Split(',');
                string[] value = Values.Split(',');

                ret += "{";
                for(int i=0;i<key.Length;i++)
                {
                    ret += "\"" + key[i] + "\":\"" + value[i] + "\",";
                }

                ret = ret.Remove(ret.Length - 1);

                ret += "}";
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return ret;
        }

        //json转datatable
       public static DataTable GetDataTableByJson(string  json) 
        {
            DataTable dataTable = new DataTable();
            DataTable result = null;
            try
            {
                if (json.Contains("null"))
                {
                    json = json.Replace("null", "");
                }
                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
                javaScriptSerializer.MaxJsonLength = Int32.MaxValue;
                ArrayList arrayList = javaScriptSerializer.Deserialize<ArrayList>(json);

                if (arrayList.Count > 0)
                {
                    foreach (Dictionary<string, object> dictionary in arrayList)
                    {
                        if (dictionary.Keys.Count<string>() == 0)
                        {
                            result = dataTable;
                            return result;
                        }
                        if (dataTable.Columns.Count == 0)
                        {
                            foreach (string current in dictionary.Keys)
                            {
                                dataTable.Columns.Add(current, dictionary[current].GetType());
                            }
                        }
                        DataRow dataRow = dataTable.NewRow();
                        foreach (string current in dictionary.Keys)
                        {
                            dataRow[current] = dictionary[current];
                        }
                        dataTable.Rows.Add(dataRow);  //这里datarow都有值，但是datatable没加上
                    }
                }
            }
            catch { };
            result = dataTable;
            return result;
        }

        //datatable转json
        public static string GetJsonByDataTable(DataTable table)
        {
            var JsonString = new StringBuilder();
            try
            {
               
                if (table.Rows.Count > 0)
                {
                    JsonString.Append("[");
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        JsonString.Append("{");
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            DateTime dtDate;
                            if (DateTime.TryParse(table.Rows[i][j].ToString(), out dtDate))
                            {
                                table.Rows[i][j]= table.Rows[i][j].ToString().Replace("T"," ");
                            }
                            if (j < table.Columns.Count - 1)
                            {
                                JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                            }
                            else if (j == table.Columns.Count - 1)
                            {
                                JsonString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                            }
                        }
                        if (i == table.Rows.Count - 1)
                        {
                            JsonString.Append("}");
                        }
                        else
                        {
                            JsonString.Append("},");
                        }
                    }
                    JsonString.Append("]");
                } 
            }
            catch (Exception)
            { }
            return JsonString.ToString();
        }
    }
}
