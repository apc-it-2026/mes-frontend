using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoSendEmail
{
    static class Program
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //正式
            // client.UserToken = "39e1db63-9f88-4f66-9d52-9043564ee0f8"; //
            //测试
             client.UserToken = "a691fcf1-96e5-4a39-a7b8-2ac04cc8f153"; //
            client.Language = "en"; //
            Application.Run(new AutoSendEmail());
        }

        public static ClientClass client;
    }
}
