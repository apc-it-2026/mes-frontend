using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_CraftProductOutPut
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
            // client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            client.UserToken = "9a14791f-22ec-47c1-aac7-833bff282e6c";
            client.Language = "cn";
            Application.Run(new F_CraftProductOutPut());
        }
        public static ClientClass client;
    }
}
