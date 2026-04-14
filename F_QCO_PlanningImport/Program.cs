using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_QCO_PlanningImport
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
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.UserToken = "f6c08c02-86ba-4cad-a46f-1382e9fb7bb0";//
            client.Language = "en";
            Application.Run(new QCOPlanImport());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}

