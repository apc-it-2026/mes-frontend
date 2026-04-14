using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Equipment_Maintenance
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
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.UserToken = "46dfa86d-c005-422a-80a0-33356a0fd2bf";//
            Client.Language = "en";
            Application.Run(new Equipment_MaintenanceMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
