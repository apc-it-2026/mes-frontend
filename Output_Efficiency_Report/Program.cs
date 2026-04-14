using PO_Completion_Upload_Data;
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Output_Efficiency_Report
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
            Client.UserToken = "d1543a88-34dc-4ef8-a6a4-493759f54f5e";
            Client.Language = "en";
            //Application.Run(new LoadingProgress());
            Application.Run(new Output_Efficiency_Report());
        }
        public static ClientClass Client;
    }
}
