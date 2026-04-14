using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;


namespace SJeMES_Framework.WebAPI
{
    public class ResponseToJson
    {
        public static HttpResponseMessage ToHttpResponse(string val)
        {
            HttpResponseMessage result = new HttpResponseMessage
            {
                Content = new StringContent(val, Encoding.GetEncoding("UTF-8"),
                 "application/x-www-form-urlencoded"),
            };

            return result;
        }
    }
}
