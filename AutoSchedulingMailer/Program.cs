using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSchedulingMailer
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new SJeMES_Framework.Class.ClientClass();
            //client.APIURL = "http://localhost:60626/api/CommonCall";
            client.APIURL = "http://10.3.0.24:8082//api/commoncall";//APC Formal Library
            client.UserToken = "5b844754-ec0a-497c-ab82-5caeb6ce2afb"; //Username : 998877  Pwd: 998877
            client.Language = "en";
            Application.Run(new AutoSchedulingMailer());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
