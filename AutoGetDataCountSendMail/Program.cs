using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace AutoGetDataCountSendMail
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
            client.APIURL = "http://10.2.1.46:8090/api/CommonCall";//正式服务器
            //client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.UserToken = "06882097-7f48-4594-870d-0200c928a009"; //开发库自动服务的账号
            client.UserToken = "d3fc8720-8bf6-4079-b7cd-5eba3f64cd11"; //正式库自动服务的账号10001,密码是mes2021
            client.Language = "en"; 
            Application.Run(new AutoGetDataCountSendMailForm());
        }

        public static ClientClass client;
    }
}