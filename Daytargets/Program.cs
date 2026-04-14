using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DayTargets
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
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 67470
            client.Language = "en";
            Application.Run(new DayTargetsForm());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}
