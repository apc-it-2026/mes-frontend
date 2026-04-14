using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Shutdown_Record
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
            Client.UserToken = "09f3f91a-b9a1-43f9-b2f8-d3dc14366d96";//
            Client.Language = "en";
            Application.Run(new Shutdown_RecordMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
