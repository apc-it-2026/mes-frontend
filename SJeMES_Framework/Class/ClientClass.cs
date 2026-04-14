using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Class
{
    public class ClientClass
    {

        public  string Language;
        public  string CompanyCode;
        public  string CompanyName;
        public  string APIURL;
        public  string UserCode;
        public  string UserName;
        public  string UserToken;
        public string WebServiceUrl;
        public string FormName;
        public OrgClass Org; 
        public string PicUrl;
        public string UploadUrl;
        


        public  Dictionary<string, object> GetDataTable(string sql, string where,
            string orderby, string pagerow, string page)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> retData = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("where", where);
            data.Add("orderby", orderby);
            data.Add("pagerow", pagerow);
            data.Add("page", page);
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetDataTableCupyPage", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                retData.Add("Total", Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["total"].ToString());
                retData.Add("Data", Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["data"].ToString()));

                return retData;


            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public System.Data.DataTable GetDT(string sql)
        {

            System.Data.DataTable dt = null;
            try
            {
                SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

                Dictionary<string, object> retData = new Dictionary<string, object>();

                Dictionary<string, object> data = new Dictionary<string, object>();
                data.Add("sql", sql);

                string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                     "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetDT", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

                var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

                if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
                {

                     dt= Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(ret["RetData"].ToString());

                    return dt;

                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return dt;

        }


        public System.Data.DataTable GetDataTable(string sql)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> retData = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("where", string.Empty);
            data.Add("orderby", string.Empty);
            data.Add("pagerow", "1000000");
            data.Add("page", "1");
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "GetDataTable", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["data"].ToString());



            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public int ExecuteNonQuery(string sql)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

         

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "ExecuteNonQuery", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                
                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(ret["RetData"].ToString());


            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }


        public Dictionary<string, object> SYSGetDataTable(string sql, string where,
            string orderby, string pagerow, string page)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> retData = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("where", where);
            data.Add("orderby", orderby);
            data.Add("pagerow", pagerow);
            data.Add("page", page);
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDataTableCupyPage", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                retData.Add("Total", Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["total"].ToString());
                retData.Add("Data", Newtonsoft.Json.JsonConvert.DeserializeObject<System.Data.DataTable>(
                    Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData"].ToString())["data"].ToString()));

                return retData;


            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }

        public System.Data.DataTable SYSGetDataTable(string sql)
        {
            DataTable dt = new DataTable();
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();

            Dictionary<string, object> retData = new Dictionary<string, object>();

            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("where", string.Empty);
            data.Add("orderby", string.Empty);
            data.Add("pagerow", "1000000");
            data.Add("page", "1");
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSGetDataTable", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {
                var dic = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(ret["RetData1"].ToString());
                dt = Newtonsoft.Json.JsonConvert.DeserializeObject<DataTable>(dic["data_dt"].ToString());
            }

            return dt;
            //else
            //{
            //    throw new Exception(ret["ErrMsg"].ToString());
            //}

        }

        public int SYSExecuteNonQuery(string sql)
        {
            SJeMES_Framework.Web.JSONPanelClassHList j = new SJeMES_Framework.Web.JSONPanelClassHList();



            Dictionary<string, object> data = new Dictionary<string, object>();
            data.Add("sql", sql);
            data.Add("sqlp", string.Empty);
            data.Add("pname", string.Empty);
            string retdata = SJeMES_Framework.WebAPI.WebAPIHelper.Post(this.APIURL,
                 "SJ_SYSAPI", "SJ_SYSAPI.DataBase", "SYSExecuteNonQuery", this.UserToken, Newtonsoft.Json.JsonConvert.SerializeObject(data));

            var ret = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(retdata);

            if (Convert.ToBoolean(ret["IsSuccess"].ToString()))
            {

                return Newtonsoft.Json.JsonConvert.DeserializeObject<int>(ret["RetData"].ToString());


            }
            else
            {
                throw new Exception(ret["ErrMsg"].ToString());
            }

        }
    }
}
