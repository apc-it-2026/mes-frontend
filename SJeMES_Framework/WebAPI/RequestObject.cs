using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJeMES_Framework.WebAPI
{
    public class RequestObject
    {
        public string DllName; //调用的DLL
        public string ClassName; //调用的Class
        public string Method; //调用的方法
        public string IP4; //请求的IP4地址
        public string MAC; //请求的MAC地址
        public bool IsRasRequst; //是否请求加密
        public bool IsRasResult;  //是否返回加密
        public string RasResultKey; //返回加密的公钥
        public string UserToken; //用户令牌
        public object Data; //请求的数据


        public RequestObject(string DllName, string ClassName, string Method, string IP4, string MAC, bool IsRasRequst, string RasResultKey, bool IsRasResult, string UserToken, object Data)
        {
            this.DllName = DllName;
            this.ClassName = ClassName;
            this.Method = Method;
            this.IP4 = IP4;
            this.MAC = MAC;
            this.IsRasRequst = IsRasRequst;
            this.RasResultKey = RasResultKey;
            this.IsRasResult = IsRasResult;
            this.UserToken = UserToken;
            this.Data = Data;
        }

        public RequestObject()
        {

        }
    }
}