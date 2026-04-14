using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RTL_Manual_In_Out
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            Client.UserToken = "d708cc4b-5f22-4f93-b5ef-81056ddcc99a";
            Client.Language = "en";
            Application.Run(new RTL_Manual_In_Out());
        }
        public static ClientClass Client;
    }
}
