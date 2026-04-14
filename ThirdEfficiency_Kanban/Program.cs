using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThirdEfficiency_Kanban
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            // Client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            // Client.UserToken = "e6e6514e-0fa4-4d9a-b504-cb2c49360a8f";//
            Client.UserToken = "b0062c66-d559-437d-b0c6-ef0497a6cff0";//
            //  Client.UserToken = "869c9290-69ba-4819-98b1-8d8e52d44b91";//
            Client.Language = "en";
            Application.Run(new ThirdEfficiency_Kanban());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
