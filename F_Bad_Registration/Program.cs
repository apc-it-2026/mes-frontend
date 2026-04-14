using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_Bad_Registration
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            // client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //client.UserToken = "2ccf9b73-6260-424b-99cd-f45af2834e80";
            client.UserToken = "fead6973-5e43-4206-a1dd-c2e79f2299af";
            client.Language = "cn";
            //Application.Run(new F_Bad_RegistrationV1());
            Application.Run(new BadRegistration());
        }
        public static ClientClass client;
    }
}
