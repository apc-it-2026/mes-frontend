using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingCalendarReport
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                client = new SJeMES_Framework.Class.ClientClass();
                client.APIURL = "http://localhost:60626/api/CommonCall";
                //client.APIURL = "http://10.2.171.110:2222/api/CommonCall";//APE测试库
                client.UserToken = "2bcabcf7-b9c8-4101-91e4-75819097c18f";
                client.Language = "en";
                Application.Run(new WorkingCalendarForm());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
