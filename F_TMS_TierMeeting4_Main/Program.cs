using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierMeeting
{
    static class Program
    {
        public static SJeMES_Framework.Class.ClientClass client;

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
            //client.APIURL = "http://10.30.1.191:8082//api/CommonCall";
            //client.UserToken = "869c9290-69ba-4819-98b1-8d8e52d44b91";
           client.UserToken = "92eca3d8-2e72-45a8-b0fa-8c30693afb85";
            client.Language = "cn"; 
            Application.Run(new IntroForm());
        }
    }
}
