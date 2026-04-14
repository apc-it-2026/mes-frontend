using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace ProcessAllocation
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
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//development server
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "67cbd401-b8e8-4d94-999a-7b39dd796386"; //Development library account
            //client.UserCode = "59299";//这种不行，因为不支持这种用法。
            client.Language = "en";
            Application.Run(new ProcessAllocation());
        }

        public static ClientClass client;
    }
}