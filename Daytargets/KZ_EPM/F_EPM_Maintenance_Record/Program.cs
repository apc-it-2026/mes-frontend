using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Maintenance_Record
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
            Client.UserToken = "66d5ced6-20cf-4cb3-9d45-6b75f9627ffa";//
            Client.Language = "en";
            Application.Run(new Maintenance_RecordMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
