using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MonthlyModelUpload
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
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            client.UserToken = "2bf125fe-b086-4779-a7d6-4a34268331e2"; //real library 938657
            client.Language = "en";
            Application.Run(new MonthlyModelUpload());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
