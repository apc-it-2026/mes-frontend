using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SFC_Holiday_Date_Report
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
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//开发服务器
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "da5c4a5b-85ba-4a57-86d5-fa848eebc503"; //开发库账号
            //client.UserCode = "59299";//这种不行，因为不支持这种用法。
            client.Language = "cn";
            Application.Run(new Holiday_DateForm());
        }
        public static ClientClass client;
    }
}
