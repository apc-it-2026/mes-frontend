using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace CuttingAllocation
{
    static class Program
    {
        public static ClientClass client;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            client = new ClientClass();
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "dfcf7038-64e0-4bd0-a474-a068f0829708"; //Real Time   938657
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //Test 938657
            client.Language = "en";

            Application.Run(new CuttingAllocation());
        }
    }
}