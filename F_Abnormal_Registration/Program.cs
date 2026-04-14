using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Abnormal_Registration
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.171.110:80//api/CommonCall";
            client.UserToken = "903a5a48-1aa1-4a53-8ad3-896db7c54ed1";
            client.Language = "cn";
            Application.Run(new F_Abnormal_Registration());
        }
        public static ClientClass client;
    }
}
