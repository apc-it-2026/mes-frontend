using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Class;
using SJeMES_Framework;


namespace Ready_To_Load
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
            Client.UserToken = "14c3380d-bc32-4813-a478-38756902cfaf";
            Client.Language = "en";
            Application.Run(new Treatment_Process());
        }

        public static ClientClass Client;

    }
}
