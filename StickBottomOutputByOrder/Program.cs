using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StickBottomOutputByOrder
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
            Client = new SJeMES_Framework.Class.ClientClass(); 
            //Client.APIURL = "http://10.2.171.110:80/api/CommonCall";
             Client.APIURL = "http://localhost:60626/api/CommonCall";
            //Client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            Client.UserToken = "f94dae81-955e-4219-a7d3-e02c47842a4a";//
            Client.Language = "en";
            Application.Run(new StickBottomOutputByOrder());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
