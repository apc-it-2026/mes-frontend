using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace WorkingHoursQuery
{
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//Development server, this is only used during development, the official library will read the data in Config.json.
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.UserToken = "0706c14d-d43c-44f5-9375-3a37a7541271";//development library
            client.UserToken = "4682a4d0-b3a6-418a-af50-432513ae73e0"; //Formal library
            client.Language = "en";
            Application.Run(new WorkingHoursQuery());
        }

        public static ClientClass client;
    }
}