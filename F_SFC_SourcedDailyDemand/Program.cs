using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SFC_SourcedDailyDemand
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
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            // client.UserToken = "c52fd6f9-e38d-4732-af25-a8144a15bfca";//正式
            client.UserToken = "92eca3d8-2e72-45a8-b0fa-8c30693afb85";
            client.Language = "cn";
            Application.Run(new F_SFC_SourcedDailyDemand());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
