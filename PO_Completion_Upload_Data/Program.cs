using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace PO_Completion_Upload_Data
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
            Client.UserToken = "6ec32c2c-8f50-4ce1-9709-2296f9cd7455";
            Client.Language = "en";
            Application.Run(new PO_Completion_Upload_Data());
        }
        public static ClientClass Client;
    }
}
