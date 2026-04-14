using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SJeMES_Framework.Common
{
    public static class Times
    {
        /// <summary>
        /// 获取响应（https://www.baidu.com）时间，没有则本地时间
        /// </summary>
        /// <returns></returns>
        public static string DateTimeS()
        {//获取网络时间
            WebRequest request = null;
            WebResponse response = null;
            WebHeaderCollection headerCollection = null;
            string datetime = string.Empty;
            try
            {
                request = WebRequest.Create("https://www.baidu.com");
                request.Timeout = 3000;
                request.Credentials = CredentialCache.DefaultCredentials;
                response = request.GetResponse();
                headerCollection = response.Headers;
                foreach (var h in headerCollection.AllKeys)
                {
                    if (h == "Date")
                    {
                        datetime = headerCollection[h];
                    }
                }
                if (!string.IsNullOrEmpty(datetime))
                {
                    return datetime;
                }
                //如果没有网络时间就返回本地时间
                else
                {

                    return DateTime.Now.ToString();
                }

            }
            catch (Exception) { return DateTime.Now.ToString(); }
            finally
            {
                if (request != null)
                { request.Abort(); }
                if (response != null)
                { response.Close(); }
                if (headerCollection != null)
                { headerCollection.Clear(); }
            }
        }
    }
}
