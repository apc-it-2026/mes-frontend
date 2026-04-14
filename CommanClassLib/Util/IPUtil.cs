using System.Net;

namespace CommanClassLib.Util
{
    public static class IPUtil
    {
        public static string GetIpAddress()
        {
            string hostName = Dns.GetHostName(); //获取本机名
            IPHostEntry localhost = Dns.GetHostEntry(hostName); //获取IPv6地址
            IPAddress localaddr = localhost.AddressList[0];
            return localaddr.ToString();
        }
    }
}