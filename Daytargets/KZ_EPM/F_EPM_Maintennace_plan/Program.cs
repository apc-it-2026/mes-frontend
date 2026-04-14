using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Maintennace_Plan
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
            Client = new SJeMES_Framework.Class.ClientClass();
            //Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.APIURL = "http://10.3.0.29:8092/api/CommonCall";
            Client.UserToken = "281b1971-1830-438f-b39a-a3005872616b";//
            Client.Language = "en";
            Application.Run(new Maintenance_PlanMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
