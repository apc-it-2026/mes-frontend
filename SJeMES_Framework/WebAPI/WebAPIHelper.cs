using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.WebAPI
{
    public class WebAPIHelper
    { 
        /// <summary>
        /// 获取WebAPI请求的参数
        /// </summary>
        /// <param name="Keys">参数名，用逗号分隔</param>
        /// <param name="ReqObj"></param>
        /// <returns></returns>
        public static Dictionary<string,object> GetWebParameters(string Keys, RequestObject ReqObj)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();
            string key2 = string.Empty;
            try
            {
                string[] split = new string[1] { "," };
                string[] keys = Keys.Split(split, StringSplitOptions.RemoveEmptyEntries);

                var js = new System.Web.Script.Serialization.JavaScriptSerializer();
                var jarr = js.Deserialize<Dictionary<string, object>>(ReqObj.Data.ToString());

                foreach (string key in keys)
                {
                    key2 = key;
                    P.Add(key, jarr[key].ToString().Trim());
                }
            }
            catch
            {
                throw new Exception("The request is missing a parameter:" + key2);
            }
            return P;
        }

        /// <summary>
        /// 获取WebAPI请求的参数
        /// </summary>
        /// <param name="Keys">参数名</param>
        /// <param name="ReqObj"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetWebParameters(List<string> Keys, RequestObject ReqObj)
        {
            Dictionary<string, object> P = new Dictionary<string, object>();
            try
            {
               

                var js = new System.Web.Script.Serialization.JavaScriptSerializer();
                var jarr = js.Deserialize<Dictionary<string, object>>(ReqObj.Data.ToString());

                foreach (string key in Keys)
                {
                    P.Add(key, jarr[key].ToString().Trim());
                }
            }
            catch
            {
                throw new Exception("The request is missing a parameter");
            }
            return P;
        }


        private static string HttpPost(string url, string body)
        {
            string result = string.Empty;
            string configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
            int sIndex = configstring.IndexOf("{");
            int eIndex = configstring.IndexOf("}");
            if (!string.IsNullOrEmpty(configstring) && sIndex >= 0 && eIndex >= 0)
            {
                configstring = configstring.Substring(sIndex, eIndex + 1);
                Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);
                string Language = Pconfig["language"];
                Encoding encoding = Encoding.UTF8;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Accept = "text/html, application/xhtml+xml, */*";
                request.ContentType = "application/json";
                request.Headers.Add("lanauage", Language);
                byte[] buffer = encoding.GetBytes(body);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    result = reader.ReadToEnd();
                }
            }
            else
                
                throw new Exception("config.json The configuration file does not exist, please check！");

            return result;
        }

        public static string Post(string url, RequestObject obj)
        {
            string body = Newtonsoft.Json.JsonConvert.SerializeObject(obj);

            return HttpPost(url, body);
        }

        public static string Post(string url,string DllName,string ClassName,string Method,string IP4,string MAC,bool IsRasRequst,bool IsRasResult,string RasResultKey,string UserToken,string Data)
        {


            SJeMES_Framework.WebAPI.RequestObject obj = new SJeMES_Framework.WebAPI.RequestObject(
                DllName, ClassName, Method,
                IP4, MAC, IsRasRequst, RasResultKey, IsRasResult, UserToken, Data);
            return Post(url, obj);

        }


        public static string Post(string url, string DllName, string ClassName, string Method,  bool IsRasRequst, bool IsRasResult, string RasResultKey, string UserToken, string Data)
        {  

            SJeMES_Framework.WebAPI.RequestObject obj = new SJeMES_Framework.WebAPI.RequestObject(
                DllName, ClassName, Method,
                Common.PCSystem.IP4(), Common.PCSystem.macId(), IsRasRequst, RasResultKey, IsRasResult, UserToken, Data);
            return Post(url, obj);

        }

        public static string Post(string url, string DllName, string ClassName, string Method,  string UserToken, string Data)
        { 
                SJeMES_Framework.WebAPI.RequestObject obj = new SJeMES_Framework.WebAPI.RequestObject(
                    DllName, ClassName, Method,
                    Common.PCSystem.IP4(), Common.PCSystem.macId(), false, string.Empty, false, UserToken, Data);
                return Post(url, obj); 
        }

       
    }
}
