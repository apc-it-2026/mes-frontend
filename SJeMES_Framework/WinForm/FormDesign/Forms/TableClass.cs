using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SJeMES_Framework.WinForm.FormDesign.Forms
{
    public class TableClass
    {
        public string TableName;
        public string IDKey;
        public List<string> Keys;
        public string SearchSQL;
        public List<string> RetrunKeys;
        public List<string> OrderKeys;
        public List<string> OrderTypes;
        public bool IsOtherTable;

        public System.Data.DataRow ShowDataRow;
        public System.Data.DataRow DataRow;
        public System.Data.DataTable ShowDataTable;
        public System.Data.DataTable DataTable;
        public System.Data.IDataReader DataReader;

        public string SqlWhere = string.Empty;


        public TableClass(string XML)
        {
            TableName = Common.StringHelper.GetDataFromFirstTag(XML, "<TableName>", "</TableName");
            IDKey = Common.StringHelper.GetDataFromFirstTag(XML, "<IDKey>", "</IDKey>");
            Keys = new List<string>();

            string strTMP = Common.StringHelper.GetDataFromFirstTag(XML, "<Keys>", "</Keys>");
            if(strTMP.LastIndexOf(",")>-1)
            {
                string[] s = strTMP.Split(',');
                foreach(string ss in s)
                {
                    Keys.Add(ss);
                }
            }
            else
            {
                Keys.Add(strTMP);
            }

            SearchSQL = Common.StringHelper.GetDataFromFirstTag(XML, "<SearchSQL>", "</SearchSQL>");

            RetrunKeys = new List<string>();
            string strRetrunKeys = Common.StringHelper.GetDataFromFirstTag(XML, "<RetrunKeys>", "</RetrunKeys>");
            if (strRetrunKeys.LastIndexOf(",") > -1)
            {
                string[] s = strRetrunKeys.Split(',');
                foreach (string ss in s)
                {
                    RetrunKeys.Add(ss);
                }
            }
            else
            {
                RetrunKeys.Add(strRetrunKeys);
            }


            OrderKeys = new List<string>();
            string strOrderKeys = Common.StringHelper.GetDataFromFirstTag(XML, "<OrderKeys>", "</OrderKeys>");
            if (strOrderKeys.LastIndexOf(",") > -1)
            {
                string[] s = strOrderKeys.Split(',');
                foreach (string ss in s)
                {
                    OrderKeys.Add(ss);
                }
            }
            else
            {
                OrderKeys.Add(strOrderKeys);
            }

            OrderTypes = new List<string>();
            string strOrderTypes = Common.StringHelper.GetDataFromFirstTag(XML, "<OrderTypes>", "</OrderTypes>");
            if (strOrderTypes.LastIndexOf(",") > -1)
            {
                string[] s = strOrderTypes.Split(',');
                foreach (string ss in s)
                {
                    OrderTypes.Add(ss);
                }
            }
            else
            {
                OrderTypes.Add(strOrderTypes);
            }
        }
        
        public System.Data.DataTable GetDataTable(string WebServiceUrl, string strWhere,int PageNum,int PageRowsCount)
        {
          
            string OrderBy = string.Empty;
            for(int i=0;i<this.OrderKeys.Count;i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if(i<this.OrderKeys.Count-1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY "+OrderBy+@") AS '行号',
*
FROM
(
SELECT
*
FROM
["+this.TableName+@"] WHERE 1=1 "+SqlWhere+@"') TT
where 1=1 " + strWhere + @"
";


            SQL = @"
SELECT TOP("+PageRowsCount+@") * 
FROM 
        (
" + SQL + @"
        ) TMP
WHERE TMP.行号 > "+PageRowsCount+"*(" + PageNum + @"-1)
";
            DataTable =ShowDataTable = Common.WebServiceHelper.GetDataTable(WebServiceUrl,SQL, new Dictionary<string, string>());
            DataTable.TableName= ShowDataTable.TableName = this.TableName;
         
            return ShowDataTable;
        }

        public System.Data.DataTable GetDataTable(Class.OrgClass Org, string WebServiceUrl, string strWhere, int PageNum, int PageRowsCount)
        {

            string OrderBy = string.Empty;
            for (int i = 0; i < this.OrderKeys.Count; i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if (i < this.OrderKeys.Count - 1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY " + OrderBy + @") AS '行号',
*
FROM
(
SELECT
*
FROM
[" + this.TableName + @"] WHERE 1=1 "+SqlWhere+@") TT
where 1=1 " + strWhere + @"
";


            SQL = @"
SELECT TOP(" + PageRowsCount + @") * 
FROM 
        (
" + SQL + @"
        ) TMP
WHERE TMP.行号 > " + PageRowsCount + "*(" + PageNum + @"-1)
";
            DataTable= ShowDataTable = Common.WebServiceHelper.GetDataTable(Org,WebServiceUrl, SQL, new Dictionary<string, string>());
            DataTable.TableName= ShowDataTable.TableName = this.TableName;
            return ShowDataTable;
        }

        public System.Data.DataTable GetShowDataTable(Class.OrgClass Org, string WebServiceUrl,string sql, int PageNum, int PageRowsCount)
        {

            string OrderBy = string.Empty;
            for (int i = 0; i < this.OrderKeys.Count; i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if (i < this.OrderKeys.Count - 1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY  " + OrderBy + @") AS '行号',
*
FROM
(" + sql + @") T
";


            SQL = @"
SELECT TOP(" + PageRowsCount + @") * 
FROM 
        (
" + SQL + @"
        ) TMP
WHERE TMP.行号 > " + PageRowsCount + "*(" + PageNum + @"-1)
";
            DataTable = ShowDataTable = Common.WebServiceHelper.GetDataTable(Org, WebServiceUrl, SQL, new Dictionary<string, string>());
            DataTable.TableName = ShowDataTable.TableName = this.TableName;
            return ShowDataTable;
        }

        public int GetRowsCount(string WebServiceUrl, string strWhere)
        {
            int retData = 0;
            string OrderBy = string.Empty;
            for (int i = 0; i < this.OrderKeys.Count; i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if (i < this.OrderKeys.Count - 1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY " + OrderBy + @") AS '行号',
*
FROM
(SELECT
*
FROM
[" + this.TableName + @"] WHERE 1=1 "+SqlWhere+@") TT
where 1=1 " + strWhere + @"
";


            SQL = @"
SELECT COUNT(*)
FROM 
        (
" + SQL + @"
        ) TMP
";
            retData = Convert.ToInt32(Common.WebServiceHelper.GetString(WebServiceUrl, SQL, new Dictionary<string, string>()));

            return retData;
        }

        public int GetRowsCount(Class.OrgClass Org, string WebServiceUrl, string strWhere)
        {
            int retData = 0;
            string OrderBy = string.Empty;
            for (int i = 0; i < this.OrderKeys.Count; i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if (i < this.OrderKeys.Count - 1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY " + OrderBy + @") AS '行号',
*
FROM
[" + this.TableName + @"]
where 1=1 " + strWhere + @"
";


            SQL = @"
SELECT COUNT(*)
FROM 
        (
" + SQL + @"
        ) TMP
";
            retData = Convert.ToInt32(Common.WebServiceHelper.GetString(Org,WebServiceUrl, SQL, new Dictionary<string, string>()));

            return retData;
        }

        public int GetShowRowsCount(Class.OrgClass Org, string WebServiceUrl,string sql)
        {
            int retData = 0;
            string OrderBy = string.Empty;
            for (int i = 0; i < this.OrderKeys.Count; i++)
            {
                OrderBy += this.OrderKeys[i] + " " + this.OrderTypes[i];
                if (i < this.OrderKeys.Count - 1)
                {
                    OrderBy += ",";
                }
            }
            string SQL = @"
SELECT 
ROW_NUMBER() OVER (ORDER BY  " + OrderBy + @")  AS '行号',
*
FROM
(" + sql + @") T
";


            SQL = @"
SELECT COUNT(*)
FROM 
        (
" + SQL + @"
        ) TMP
";
            retData = Convert.ToInt32(Common.WebServiceHelper.GetString(Org, WebServiceUrl, SQL, new Dictionary<string, string>()));

            return retData;
        }

        public System.Data.DataRow GetDataTableRow(string WebServiceUrl, string wheresql)
        {

            string sql = @"
SELECT
*
FROM (SELECT * FROM ["+TableName+@"] WHERE 1=1 "+SqlWhere+@") TT
";
            sql += wheresql;
            ShowDataTable = Common.WebServiceHelper.GetDataTable( WebServiceUrl,sql, new Dictionary<string, string>());
            ShowDataTable.TableName = this.TableName;
            if (ShowDataTable.Rows.Count > 0)
            {
                DataRow= ShowDataRow = ShowDataTable.Rows[0];
            }
            else
            {
                DataRow= ShowDataRow = ShowDataTable.NewRow();
            }
            return ShowDataRow;
        }

        public System.Data.DataRow GetDataTableRow(Class.OrgClass Org, string WebServiceUrl, string wheresql)
        {
            

            string sql = @"
SELECT
TOP(1)
*
FROM (SELECT * FROM [" + TableName + @"] WHERE 1=1 "+SqlWhere+@") TT
";
           

            sql += wheresql;
            ShowDataTable = Common.WebServiceHelper.GetDataTable(Org,WebServiceUrl, sql, new Dictionary<string, string>());
            ShowDataTable.TableName = this.TableName;
            if (ShowDataTable.Rows.Count > 0)
            {
                DataRow= ShowDataRow = ShowDataTable.Rows[0];
            }
            else
            {
                DataRow= ShowDataRow = ShowDataTable.NewRow();
            }
            return ShowDataRow;
        }



        public System.Data.DataRow GetDataFisrtRow(string WebServiceUrl)
        {
            string order = string.Empty;
            string sql = @"
SELECT
Top(1) *
FROM (SELECT * FROM [" + TableName + @"] WHERE 1=1 "+SqlWhere+@") TT 
";
            for(int i=0;i<OrderKeys.Count;i++)
            {
                if(string.IsNullOrEmpty(order))
                {
                    if (OrderKeys[i].IndexOf("[") > -1 && OrderKeys[i].IndexOf("]") > -1)
                    {
                        order += " ORDER BY [" + 
                        SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(OrderKeys[i], "[", "]") + "] " + OrderTypes[i] + " ";
                    }
                    else
                    {
                        order += " ORDER BY [" +
                        SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(OrderKeys[i], "[", "]")  + "] " + OrderTypes[i] + " ";
                    }
                }
                else
                {
                    if (OrderKeys[i].IndexOf("[") > -1 && OrderKeys[i].IndexOf("]") > -1)
                    {
                        order += ", [" + OrderKeys[i] + "] " + OrderTypes[i] + " ";
                    }
                    else
                    {
                        order += ", [" + OrderKeys[i] + "] " + OrderTypes[i] + " ";
                    }
                    
                }
            }
            sql += order;
            ShowDataTable = Common.WebServiceHelper.GetDataTable(WebServiceUrl,sql, new Dictionary<string, string>());
            ShowDataTable.TableName = this.TableName;
            if (ShowDataTable.Rows.Count > 0)
            {
                DataRow= ShowDataRow = ShowDataTable.Rows[0];
            }
            else
            {
                DataRow= ShowDataRow = ShowDataTable.NewRow();
            }
            return ShowDataRow;
        }

        public System.Data.DataRow GetDataFisrtRow(Class.OrgClass Org, string WebServiceUrl)
        {
            string order = string.Empty;
            string sql = @"
SELECT
Top(1) *
FROM (SELECT * FROM [" + TableName + @"] WHERE 1=1 "+SqlWhere+@") TT
";
            for (int i = 0; i < OrderKeys.Count; i++)
            {
                if (string.IsNullOrEmpty(order))
                {
                    if (OrderKeys[i].IndexOf("[") > -1 && OrderKeys[i].IndexOf("]") > -1)
                    {
                        order += " ORDER BY [" +
                        SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(OrderKeys[i], "[", "]") + "] " + OrderTypes[i] + " ";
                    }
                    else
                    {
                        order += " ORDER BY [" +
                        OrderKeys[i] + "] " + OrderTypes[i] + " ";
                    }
                }
                else
                {
                    if (OrderKeys[i].IndexOf("[") > -1 && OrderKeys[i].IndexOf("]") > -1)
                    {
                        order += ", [" + SJeMES_Framework.Common.StringHelper.GetDataFromFirstTag(OrderKeys[i], "[", "]") + "] " + OrderTypes[i] + " ";
                    }
                    else
                    {
                        order += ", [" + OrderKeys[i] + "] " + OrderTypes[i] + " ";
                    }

                }
            }
            sql += order;
            ShowDataTable = Common.WebServiceHelper.GetDataTable(Org,WebServiceUrl, sql, new Dictionary<string, string>());
            ShowDataTable.TableName = this.TableName;
            if (ShowDataTable.Rows.Count > 0)
            {
                DataRow= ShowDataRow = ShowDataTable.Rows[0];
            }
            else
            {
                DataRow= ShowDataRow = ShowDataTable.NewRow();
            }
            return ShowDataRow;
        }

        public System.Data.DataRow GetNewRow(SJeMES_Framework.Class.OrgClass Org, string WebServiceUrl)
        {

            if(ShowDataTable == null)
            {
                GetDataFisrtRow(Org, WebServiceUrl);
            }

            DataRow= ShowDataRow = ShowDataTable.NewRow();

           
            return ShowDataRow;
        }



        public bool AddRow(string WebServiceUrl)
        {
            bool ret = false;
            string insertKey = string.Empty;
            string insertValue = string.Empty;
            foreach (System.Data.DataColumn dc in this.DataRow.Table.Columns)
            {
                if(string.IsNullOrEmpty(insertKey))
                {
                    
                    if (!string.IsNullOrEmpty(this.DataRow[dc.ColumnName].ToString()))
                    {
                        insertKey += "[" + dc.ColumnName + @"]";
                        insertValue += "'" + this.DataRow[dc.ColumnName] + @"'";
                    }
                    
                }
                else
                {
                    

                    if (!string.IsNullOrEmpty(this.DataRow[dc.ColumnName].ToString()))
                    {
                        insertKey += ",[" + dc.ColumnName + @"]";
                        insertValue += ",'" + this.DataRow[dc.ColumnName] + @"'";
                    }
 
                }
            }
            string sql = @"
INSERT INTO ["+this.TableName+@"]
("+insertKey+@")
Values
("+insertValue+@")
";

            Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(WebServiceUrl, sql, new Dictionary<string, string>());

            if(Convert.ToBoolean(obj["IsSuccess"]))
            {
                ret = true;

                string whereStr = string.Empty;

                foreach(string s in this.Keys)
                {
                    if(string.IsNullOrEmpty(whereStr))
                    {
                        whereStr += " WHERE ["+s+"]='"+this.DataRow[s].ToString()+@"'";
                    }
                    else
                    {
                        whereStr += " AND [" + s + "]='" + this.DataRow[s].ToString() + @"'";
                    }
                }

                DataRow= this.ShowDataRow = GetDataTableRow(WebServiceUrl, whereStr);
            }
            return ret; 
        }

        public bool AddRow(Class.OrgClass Org, string WebServiceUrl)
        {
            bool ret = false;
            string insertKey = string.Empty;
            string insertValue = string.Empty;

            string sqlSelect = string.Empty;
            sqlSelect = @"
SELECT TOP(1) * FROM
["+this.TableName+@"]
WHERE 1=1
";
            foreach(string key in this.Keys)
            {
                sqlSelect += " AND [" + key + @"] = '" + this.DataRow[key].ToString() + @"' ";
            }

            if (Common.WebServiceHelper.GetDataTable(Org, WebServiceUrl, sqlSelect, new Dictionary<string, string>()).Rows.Count == 0)
            {

                foreach (System.Data.DataColumn dc in this.DataRow.Table.Columns)
                {
                    if (string.IsNullOrEmpty(insertKey))
                    {


                        if (!string.IsNullOrEmpty(this.DataRow[dc.ColumnName].ToString()))
                        {
                            insertKey += "[" + dc.ColumnName + @"]";
                            insertValue += "'" + this.DataRow[dc.ColumnName] + @"'";
                        }

                    }
                    else
                    {


                        if (!string.IsNullOrEmpty(this.DataRow[dc.ColumnName].ToString()))
                        {
                            insertKey += ",[" + dc.ColumnName + @"]";
                            insertValue += ",'" + this.DataRow[dc.ColumnName] + @"'";
                        }

                    }
                }
                string sql = @"
INSERT INTO [" + this.TableName + @"]
(" + insertKey + @")
Values
(" + insertValue + @")
";

                Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(Org, WebServiceUrl, sql, new Dictionary<string, string>());

                if (Convert.ToBoolean(obj["IsSuccess"]))
                {
                    ret = true;

                    string whereStr = string.Empty;

                    foreach (string s in this.Keys)
                    {
                        if (string.IsNullOrEmpty(whereStr))
                        {
                            if (!string.IsNullOrEmpty(this.DataRow[s].ToString()))
                                whereStr += " WHERE [" + s + "]='" + this.DataRow[s].ToString() + @"'";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(this.DataRow[s].ToString()))
                                whereStr += " AND [" + s + "]='" + this.DataRow[s].ToString() + @"'";
                        }
                    }

                    DataRow = this.ShowDataRow = GetDataTableRow(Org, WebServiceUrl, whereStr);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("已存在相同主键的数据，不能重复添加");
            }
            return ret;
        }


        

        public bool EditRow(string WebServiceUrl)
        {
            bool ret = false;
            string UpdateString = string.Empty;
            string WhereString = string.Empty;
            foreach (System.Data.DataColumn dc in this.DataRow.Table.Columns)
            {
                if (dc.ColumnName != "id" && dc.ColumnName != "guid" && dc.ColumnName != "timestamp" && dc.ColumnName !="行号")
                {
                    if (!string.IsNullOrEmpty(this.DataRow[dc.ColumnName].ToString()))
                    {

                        if (string.IsNullOrEmpty(UpdateString))
                        {
                            UpdateString += "[" + dc.ColumnName + @"]='" + this.DataRow[dc.ColumnName] + @"'";
                        }
                        else
                        {
                            UpdateString += ",[" + dc.ColumnName + @"] = '" + this.DataRow[dc.ColumnName] + @"'";
                        }
                    }
                   
                }
            }
            string sql = @"
UPDATE  ["+this.TableName+@"]
SET
"+UpdateString+@"
WHERE id ="+this.DataRow["id"].ToString()+@"
";

            Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(WebServiceUrl, sql, new Dictionary<string, string>());

            if (Convert.ToBoolean(obj["IsSuccess"]))
            {
                ret = true;
            }
            return ret;
        }

        public bool EditRow(Class.OrgClass Org, string WebServiceUrl)
        {
            bool ret = false;
            string UpdateString = string.Empty;
            string WhereString = string.Empty;
            foreach (System.Data.DataColumn dc in this.DataRow.Table.Columns)
            {
                if (dc.ColumnName != "id" && dc.ColumnName != "guid" && dc.ColumnName != "timestamp" && dc.ColumnName != "行号")
                {


                    if (string.IsNullOrEmpty(UpdateString))
                    {
                        UpdateString += "[" + dc.ColumnName + @"]='" + this.DataRow[dc.ColumnName] + @"'";
                    }
                    else
                    {
                        UpdateString += ",[" + dc.ColumnName + @"] = '" + this.DataRow[dc.ColumnName] + @"'";
                    }


                }
            }
            string sql = @"
UPDATE  [" + this.TableName + @"]
SET
" + UpdateString + @"
WHERE id =" + this.DataRow["id"].ToString() + @"
";

            Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(Org,WebServiceUrl, sql, new Dictionary<string, string>());

            if (Convert.ToBoolean(obj["IsSuccess"]))
            {
                ret = true;
            }
            return ret;
        }

        public bool DelRow(string WebServiceUrl)
        {
            bool ret = false;
            string UpdateString = string.Empty;
            string WhereString = string.Empty;
           

            if (!string.IsNullOrWhiteSpace(WhereString))
            {
                string sql = @"
DELETE  FROM [" + this.TableName + @"]
WHERE id ="+this.DataRow["id"].ToString()+@"
";

                Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(WebServiceUrl, sql, new Dictionary<string, string>());

                if (Convert.ToBoolean(obj["IsSuccess"]))
                {
                    ret = true;
                }
                
            }
            return ret;
        }

        public bool DelRow(Class.OrgClass Org, string WebServiceUrl)
        {
            bool ret = false;
            string UpdateString = string.Empty;
            string WhereString = string.Empty;



            string sql = @"
DELETE  FROM [" + this.TableName + @"]
WHERE id =" + this.DataRow["id"].ToString() + @"
";

            Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(Org, WebServiceUrl, sql, new Dictionary<string, string>());

            if (Convert.ToBoolean(obj["IsSuccess"]))
            {
                ret = true;
            }


            return ret;
        }

        public bool DelRow(Class.OrgClass Org, string WebServiceUrl,string where)
        {
            bool ret = false;
            string UpdateString = string.Empty;
            string WhereString = string.Empty;

            if (where != "WHERE 1=1")
            {

                string sql = @"
DELETE  FROM [" + this.TableName + @"] "+where+@"
";

            Dictionary<string, object> obj = Common.WebServiceHelper.ExecuteNonQuery(Org, WebServiceUrl, sql, new Dictionary<string, string>());

                if (Convert.ToBoolean(obj["IsSuccess"]))
                {
                    ret = true;
                }
            }


            return ret;
        }

        public void SetDataRow(System.Data.DataRow dr)
        {
            DataRow = this.ShowDataRow = dr;
        }
    }
}
