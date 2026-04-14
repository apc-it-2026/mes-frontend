using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SJeMES_Framework.Common
{
    public class Security
    {

        public static string GetSN(string PCSN,string APPSN)
        {
            string ret = string.Empty;

            for(int i=0;i<PCSN.Length;i++)
            {
                ret += PCSN[i] + APPSN[i];
            }

            ret = MD5(ret);

            return ret;
        }

        public static string GetPCSN()
        {
            string ret = string.Empty;

            ret = PCSystem.cpuId() + PCSystem.biosId();

            int num = 0;

            for(int i=0;i<ret.Length;i++)
            {
                try
                {
                    if (Convert.ToInt32(ret[i].ToString()) > 0)
                    {
                        num = Convert.ToInt32(ret[i].ToString());
                        break;
                    }
                }
                catch { }
            }

            for(;num>0;num--)
            {
                ret = MD5(ret);
            }

            return ret;
        }

        public static string MD5(string text)
        {


            text = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(text, "md5");


            return text;
        }
    }
}
