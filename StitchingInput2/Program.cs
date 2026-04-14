using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace StitchingInput2
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
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";//official server
            client.APIURL = "http://localhost:60626/api/CommonCall"; //local interface
            //client.APIURL = "http://10.2.171.110:80/api/CommonCall";//test server
            client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; // Development library users
            //client.UserToken = "4258b13b-b92c-46bf-9d9f-d22b5e02d05e"; // Official library user
            //client.UserToken = "26bd1183-c8f5-4b9f-ad7d-d693475d732c";// test library user
            client.Language = "en";
            client.WebServiceUrl = "http://10.3.0.24:8081/SJ-WebService.asmx";
            Application.Run(new StitchingInput2());
        }

        public static ClientClass client;

    }
}