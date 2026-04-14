using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Data;

namespace SJeMES_Framework.Common
{
    public class StringHelper
    {

        public static string GetSqlPByDictionary(Dictionary<string, object> p)
        {
            string ret = string.Empty;

            foreach(string key in p.Keys)
            {
                ret += "<" + key + ">" + p[key].ToString() + "</" + key + ">";
            }

            return ret;
        }

        public static string GetPNameByDictionary(Dictionary<string, object> p)
        {
            string ret = string.Empty;

            foreach (string key in p.Keys)
            {
                if (!string.IsNullOrEmpty(ret))
                    ret += "," + key;
                else
                    ret += key;
            }

            return ret;
        }

        public static Dictionary<string,object> CopyDataDictionary(Dictionary<string,object> DicSource,Dictionary<string,object> DicTarget)
        {
            foreach(string key in DicSource.Keys)
            {
                if(DicTarget.ContainsKey(key))
                {
                    DicTarget[key] = DicSource[key];
                }
            }

            return DicTarget;
        }

        public static Dictionary<string,object> GetDictionaryByDataRow(DataRow dr)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();

            foreach(DataColumn dc in dr.Table.Columns)
            {
                ret.Add(dc.ColumnName, dr[dc.ColumnName].ToString().Trim());
            }

            return ret;
        }

        public static string GetSqlCutPage(string DBType, string sql, string PageRow, string Page,string OrderBy)
        {
            if(string.IsNullOrEmpty(OrderBy))
            {
                OrderBy = " order by id desc ";
            }

            if (DBType.ToLower() == "mysql")
            {
                sql = @"
SELECT
*
FROM
(
SELECT
*,
@N:=@N+1 AS '行号'
FROM
(" + sql + @") TTT,
(SELECT @N:=0) R
" + OrderBy + @"
) TTTT WHERE 行号>" + (Convert.ToInt32(Page) - 1) * Convert.ToInt32(PageRow) + @"
limit " + PageRow + @" 
";
            }
            else if(DBType.ToLower() == "mysql")
            {
                sql = @"
SELECT
top("+ PageRow+@")
*
FROM
(
SELECT
*,
row_number() over(" + OrderBy + @") as rownumber
FROM
(" + sql + @") TTT
) TTTT WHERE rownumber>" + (Convert.ToInt32(Page) - 1) * Convert.ToInt32(PageRow) + @"
";
            }
            else if (DBType.ToLower() == "oracle")
            {
                sql = @"
SELECT
top(" + PageRow + @")
*
FROM
(
SELECT
*,
row_number() over(" + OrderBy + @") as rn
FROM
(" + sql + @") TTT
) TTTT where rn between " + ((Convert.ToInt32(Page) - 1) * Convert.ToInt32(PageRow) +1) + " and " + ((Convert.ToInt32(Page) - 1) * Convert.ToInt32(PageRow) + Convert.ToInt32(PageRow)).ToString();

            }

            return sql;
        }


        

        

        public static Dictionary<string,object> GetDictionaryByString(string keys)
        {
            Dictionary<string, object> ret = new Dictionary<string, object>();
            try
            {
                keys = keys.Replace("，", ",");
                string[] Keys = keys.Split(',');

                foreach(string s in Keys)
                {
                    ret.Add(s, string.Empty);
                }

            }
            catch (Exception ex) { throw ex; }

            return ret;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="Obj"></param>
        /// <param name="Where">用AND拼接</param>
        /// <returns></returns>
        public static string GetSelectSqlByDictionary(string DBType, string TableName, Dictionary<string, object> Obj,string Where)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("查询表名不能为空");
            }
            if (Obj.Count == 0)
            {
                throw new Exception("查询字段不能为空");
            }

            try
            {
                sql = @"
SELECT ";
                foreach (string key in Obj.Keys)
                {
                    sql += key + ",";
                }
                sql = sql.Remove(sql.Length - 1);

                if (DBType.ToLower() == "mysql")
                {
                    sql += " FROM `" +  TableName + @"` ";
                }
                else if (DBType.ToLower() == "sqlserver")
                {
                    sql += " FROM [" +  TableName + @"] ";
                }
                else if (DBType.ToLower() == "oracle")
                {
                    sql += " FROM \"" +  TableName + "\" ";
                }

                sql += " WHERE 1=1 " + Where;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        public static string GetSelectSqlByDictionary(string TableName, Dictionary<string, object> Obj, string Where)
        {
            

            string sql = string.Empty;

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("查询表名不能为空");
            }
            if (Obj.Count == 0)
            {
                throw new Exception("查询字段不能为空");
            }

            try
            {

                sql = @"
SELECT ";
                foreach (string key in Obj.Keys)
                {
                    sql += key + ",";
                }
                sql = sql.Remove(sql.Length - 1);
                sql += " FROM " + TableName + @" ";


                sql += " WHERE 1=1 " + Where;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        public static string GetInsertSqlByDictionary(string DBType, string TableName, DataTable Obj)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("新增表名不能为空");
            }
            if (Obj.Rows.Count == 0)
            {
                throw new Exception("新增字段不能为空");
            }

            try
            {
                sql = string.Empty; 
                foreach (DataRow dr in Obj.Rows)
                {
                    if (DBType.ToLower() == "mysql")
                    {
                        sql += @"
Insert into `" + TableName + @"`
(
";
                    }
                    else if (DBType.ToLower() == "sqlserver")
                    {
                        sql += @"
Insert into [" + TableName + @"]
(
";
                    }
                    else if (DBType.ToLower() == "oracle")
                    {
                        sql += 
"Insert into \"" + TableName + "\"( ";
                    }


                    foreach (DataColumn dc in Obj.Columns)
                    {
                        if (DBType.ToLower() == "mysql")
                        {
                            sql += "`" + dc.ColumnName + "`,";
                        }
                        else if (DBType.ToLower() == "sqlserver")
                        {
                            sql += "[" + dc.ColumnName + "],";
                        }
                        else if (DBType.ToLower() == "oracle")
                        {
                            sql += "\"" + dc.ColumnName + "\",";
                        }

                        
                    }

                    sql = sql.Remove(sql.Length - 1);

                    sql += ") VALUES (";
                    foreach (DataColumn dc in Obj.Columns)
                    {
                        sql += "'"+ dr[dc.ColumnName].ToString().Trim() + "',";
                    }

                    sql = sql.Remove(sql.Length - 1);
                    sql += ");";

                }

                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        public static string GetInsertSqlByDictionary(string DBType, string TableName, Dictionary<string, object> Obj2, DataTable Obj)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("新增表名不能为空");
            }
            if (Obj.Rows.Count == 0)
            {
                throw new Exception("新增字段不能为空");
            }

            try
            {
                sql = string.Empty;
                foreach (DataRow dr in Obj.Rows)
                {


                    if (DBType.ToLower() == "mysql")
                    {
                        sql += @"
Insert into `" + TableName + @"`
(
";
                    }
                    else if (DBType.ToLower() == "sqlserver")
                    {
                        sql += @"
Insert into [" + TableName + @"]
(
";
                    }
                    else if (DBType.ToLower() == "oracle")
                    {
                        sql +=
"Insert into \"" + TableName + "\"( ";
                    }

                    foreach (DataColumn dc in Obj.Columns)
                    {
                        sql += dc.ColumnName + ",";
                    }

                    foreach (string key in Obj2.Keys)
                    {
                        sql +="`"+ key + "`,";
                    }

                    sql = sql.Remove(sql.Length - 1);

                    

                    sql += ") VALUES (";
                    foreach (DataColumn dc in Obj.Columns)
                    {
                        sql += "'" + dr[dc.ColumnName].ToString().Trim() + "',";
                    }

                    foreach(string key in Obj2.Keys)
                    {
                        sql += "'" + Obj2[key].ToString().Trim() + "',";
                    }

                    sql = sql.Remove(sql.Length - 1);
                    sql += ");";

                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        
        public static string GetInsertSqlByDictionary(string DBType, string TableName,Dictionary<string,object> Obj)
        {
            string sql = string.Empty;

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("新增表名不能为空");
            }
            if (Obj.Count == 0)
            {
                throw new Exception("新增字段不能为空");
            }

            try
            {
                if (DBType.ToLower() == "mysql")
                {
                    sql += @"
Insert into `" + TableName + @"`
(
";
                }
                else if (DBType.ToLower() == "sqlserver")
                {
                    sql += @"
Insert into [" + TableName + @"]
(
";
                }
                else if (DBType.ToLower() == "oracle")
                {
                    sql +=
"Insert into \"" + TableName + "\"( ";
                }

                foreach (string key in Obj.Keys)
                {
                    if (DBType.ToLower() == "mysql")
                    {
                        sql += "`" + key + "`,";
                    }
                    else if (DBType.ToLower() == "sqlserver")
                    {
                        sql += "[" + key + "],";
                    }
                    else if (DBType.ToLower() == "oracle")
                    {
                        sql += "\"" + key + "\",";
                    }

                    
                }
                sql = sql.Remove(sql.Length - 1);

                sql += ") VALUES (";
                foreach (string key in Obj.Keys)
                {
                    sql +="@"+key+",";
                }
                sql = sql.Remove(sql.Length - 1);
                sql += ")";
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="UpdateWhere">不用AND连接Where</param>
        /// <param name="Obj"></param>
        /// <returns></returns>
        public static string GetUpdateSqlByDictionary(string TableName,string UpdateWhere, Dictionary<string, object> Obj)
        {
            string sql = string.Empty;
            if(string.IsNullOrEmpty(UpdateWhere))
            {
                throw new Exception("更新条件不能为空");
            }

            if (string.IsNullOrEmpty(TableName))
            {
                throw new Exception("更新表名不能为空");
            }
            if (Obj.Count==0)
            {
                throw new Exception("更新字段不能为空");
            }


            try
            {
                sql = @"
Update " + TableName + @" set
";
                foreach (string key in Obj.Keys)
                {
                    sql += key + "=@"+key+",";
                }

                sql = sql.Remove(sql.Length - 1);

                sql += @" WHERE " + UpdateWhere;


            }
            catch (Exception ex)
            {
                throw ex;
            }

            return sql;
        }

        public static string GetXMLFormDictionary(string DllName, string ClassName, string Method, Dictionary<string,string> P)
        {
            string ret = string.Empty;
            string Data = string.Empty;

            foreach (string key in P.Keys)
            {
                Data += "<" + key + ">" + P[key] + "</" + key + ">";
                Data += @"
";
            }

            try
            {
                

                string XML = string.Empty;
                string IP4 = Dns.GetHostByName(Dns.GetHostName()).AddressList[0].ToString();


                string MAC = string.Empty;


                List<string> macs = new List<string>();
                NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface ni in interfaces)
                {
                    macs.Add(ni.GetPhysicalAddress().ToString());
                }

                if (macs.Count > 0)
                {
                    MAC = macs[0];
                }


                XML = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
                <IP4>" + IP4 + @"</IP4>
                <MAC>" + MAC + @"</MAC>
                
                <Data>" + @"
                    " + Data + @"
                </Data>
            </WebServie>
            ";

                ret = XML;

            }
            catch (Exception ex)
            {
                ret = @"
            <WebServie>
                <DllName>" + DllName + @"</DllName>
                <ClassName>" + ClassName + @"</ClassName>
                <Method>" + Method + @"</Method>
               
                <Data>" + @"
                    " + Data + @"
                </Data>
                <Return>
                    <IsSuccess>False</IsSuccess>
                    <RetData>00000:" + ex.Message + @"</RetData>
                </Return>
            </WebServie>
            ";
            }

            return ret;
        }

        /// <summary>
        /// 根据标签替换标签中的内容
        /// </summary>
        /// <param name="data">全部数据</param>
        /// <param name="data2">要处理的数据</param>
        /// <param name="NewData">要替换的数据集合</param>
        /// <param name="StartTag">开始标签数据集合</param>
        /// <param name="EndTag">结束标签数据集合</param>
        /// <returns></returns>
        public static string ChangeDataFromTag(string data,string data2,List<string> NewData,List<string> StartTag,List<string> EndTag)
        {
            string ret = data+" ";

            string ret2 = data2+"   ";

            for (int i = 0; i < NewData.Count; i++)
            {
                
                int s = ret2.LastIndexOf(StartTag[i]);

                int e = ret2.LastIndexOf(EndTag[i]) + EndTag[i].Length;

                if (s > -1)
                {

                    string sTmp = ret2.Substring(s, e - s);

                    ret2 = ret2.Replace(sTmp, NewData[i]);
                }
                else
                {
                    ret2 += NewData[i];
                }
            }

            ret=ret.Replace(data2, ret2);

            return ret;
        }

        /// <summary>
        /// 根据标签获取标签中的内容
        /// </summary>
        /// <param name="StartTag">开始标签</param>
        /// <param name="EndTag">结束</param>
        /// <returns></returns>
        public static List<string> GetDataFromTag(string data, string StartTag, string EndTag)
        {
            List<string> ret = new List<string>();
            try
            {
                data = data + "         ";
                while (data.Length > 0)
                {
                    int startIndex = -1;
                    int endIndex = -1;
                    if (data.IndexOf(StartTag) > -1)
                    {
                        startIndex = data.IndexOf(StartTag) + StartTag.Length;
                    }

                    if (startIndex > -1)
                    {
                        if (data.Substring(startIndex).IndexOf(EndTag) > -1)
                        {
                            endIndex = data.Substring(startIndex).IndexOf(EndTag) + EndTag.Length;
                        }
                    }

                    if (startIndex > -1 && endIndex > -1)
                    {
                        string tmp = data.Substring(startIndex);
                        tmp = tmp.Remove(endIndex);
                        tmp = tmp.Replace(EndTag, "");
                        ret.Add(tmp);
                        data = data.Substring(startIndex).Substring(endIndex);

                    }


                    if (startIndex == -1 || endIndex == -1)
                    {
                        data = string.Empty;
                    }

                }
            }
            catch { }

            return ret;
        }


        /// <summary>
        /// 根据标签获第一个取标签中的内容
        /// </summary>
        /// <param name = "StartTag" > 开始标签 </ param >
        /// <param name="EndTag">结束</param>
        /// <returns></returns>
        public static string GetDataFromFirstTag(string data, string StartTag, string EndTag)
        {
            string ret = string.Empty;
            try
            {
                data = data + "         ";
                while (data.Length > 0)
                {
                    int startIndex = -1;
                    int endIndex = -1;
                    if (data.IndexOf(StartTag) > -1)
                    {
                        startIndex = data.IndexOf(StartTag) + StartTag.Length;
                    }

                    if (startIndex > -1)
                    {
                        if (data.Substring(startIndex).IndexOf(EndTag) > -1)
                        {
                            endIndex = data.Substring(startIndex).IndexOf(EndTag) + EndTag.Length;
                        }
                    }

                    if (startIndex > -1 && endIndex > -1)
                    {
                        string tmp = string.Empty;
                        if (data.Substring(startIndex).Length > endIndex)
                        {
                            tmp = data.Substring(startIndex).Remove(endIndex).Replace(EndTag, "");
                        }
                        else
                        {
                            tmp = data.Substring(startIndex).Replace(EndTag, "");
                        }
                        ret = tmp;

                        return ret;
                    }


                    if (startIndex == -1 || endIndex == -1)
                    {
                        data = string.Empty;
                    }

                }
            }
            catch { }

            return ret;
        }

        public static System.Data.DataTable GetDataTableFromXML(string XML)
        {
            System.Data.DataTable dt = new System.Data.DataTable();
           
            string dtXML = GetDataFromFirstTag(XML, "<DataTable>", "</DataTable>");
            string[] s = new string[1];
            s[0] = "<dt@;>";
            string[] cXML = GetDataFromFirstTag(dtXML, "<Columns>", "</Columns>").Split(s,StringSplitOptions.RemoveEmptyEntries);

            foreach(string c in cXML)
            {
                dt.Columns.Add(c.Trim());
            }

            List<string> RowXML = GetDataFromTag(dtXML, "<Row>", "</Row>");

            foreach(string r in RowXML)
            {
                string[] rowdata = r.Split(s, StringSplitOptions.None);

                System.Data.DataRow dr = dt.NewRow();

                for(int i=0;i<rowdata.Length;i++)
                {
                    dr[i] = rowdata[i];
                }

                dt.Rows.Add(dr);
            }



            return dt;
        }


        public static string GetXMLFromDataTable(System.Data.DataTable dt)
        {
            string XML = string.Empty;

            XML += "<DataTable>";

            XML += "<Columns>";
            foreach (System.Data.DataColumn c in dt.Columns)
            {
                XML += c.ColumnName + "<dt@;>";
            }
            XML = XML.Remove(XML.Length - 6);
            XML += "</Columns>";

            foreach(System.Data.DataRow dr in dt.Rows)
            {
                XML += "<Row>";

                foreach (System.Data.DataColumn c in dt.Columns)
                {
                    XML += dr[c.ColumnName].ToString() + "<dt@;>";
                }
                XML = XML.Remove(XML.Length - 6);
                XML += "</Row>";
            }



            XML += "</DataTable>";


            return XML;
        }
    }
}
