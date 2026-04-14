using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Domestic_PO_Tracking
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
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //Client.APIURL = "http://10.3.0.29:8092//api/CommonCall";
            //Client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            Client.UserToken = "988b291d-9840-40a6-a007-df4616abb388";//
            Client.Language = "en";
            Application.Run(new Domestic_PO_Tracking());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
