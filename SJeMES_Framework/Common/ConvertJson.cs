using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;

using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace SJeMES_Framework.Common
{
    public class ConvertJson<T> where T : class
    {
        #region 转换
        /// <summary>
        /// json参数 转对象
        /// </summary>
        /// <param name="json">json参数</param>
        /// <returns></returns>
        public T ToObj(string json)
        {
            T parameter = null;
            try
            {
                //参数为空
                if (string.IsNullOrEmpty(json) || json == "")
                {
                    return null;
                }

                parameter = Activator.CreateInstance<T>();
                using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(json)))
                {

                    DataContractJsonSerializer dcj = new DataContractJsonSerializer(typeof(T));
                    parameter = (T)dcj.ReadObject(ms);
                }

                if (parameter == null)
                    throw new Exception("参数错误");
            }
            catch (Exception e)
            {
                throw new Exception("参数错误:" + e.Message);
            }

            return parameter;
        }

        /// <summary>/// 
        /// 对象 转 json 
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public string ToJson(T obj)
        {
            string jsonStr;
            if (obj is string || obj is char)
            {
                jsonStr = obj.ToString();
            }
            else
            {
                //对象 转json
                //DataContractJsonSerializer json = new DataContractJsonSerializer(typeof(T));
                //using (MemoryStream stream = new MemoryStream())
                //{
                //    json.WriteObject(stream, obj);
                //    jsonStr = Encoding.UTF8.GetString(stream.ToArray());
                //}
                jsonStr = new JavaScriptSerializer().Serialize(obj);
            }

            //时间格式化
            jsonStr = Regex.Replace(jsonStr, @"\\/Date\((\d+)\)\\/", match =>
            {
                DateTime dt = new DateTime(1970, 1, 1);
                dt = dt.AddMilliseconds(long.Parse(match.Groups[1].Value));
                dt = dt.ToLocalTime();
                return dt.ToString("yyyy-MM-dd HH:mm:ss");
            });

            return jsonStr;
        }
        #endregion





     
    }
}