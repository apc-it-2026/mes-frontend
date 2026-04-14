using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace StitchingOutput2
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
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//development library
            //client.APIURL = "http://10.3.0.24:8082/api/CommonCall";//official server
            client.APIURL = "http://localhost:60626/api/CommonCall"; //local interface
#if DEBUG
            client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; // Development library users
#else
            client.UserToken = "39c69540-4fff-4ca4-9088-42507391f32c"; // Official library user
#endif
            client.Language = "en";
            client.WebServiceUrl = "http://10.3.0.24:8081/SJ-WebService.asmx";
            Application.Run(new StitchingOutput2());
        }

        public static ClientClass client;
    }
}