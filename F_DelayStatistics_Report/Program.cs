using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_DelayStatistics_Report
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
            client.UserToken = "4c445d96-d61e-486f-9e89-d10d9fc4d087";
            client.Language = "cn";
            Application.Run(new F_DelayStatistics_Report());
        }
        public static ClientClass client;
    }
}
