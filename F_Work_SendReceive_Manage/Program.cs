using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_Work_SendReceive_Manage
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
            client.UserToken = "5dcd2cbc-46a2-43f2-946e-62bef66febfb"; 
            client.Language = "cn";
            Application.Run(new F_Work_SendReceive_Manage());
        }
        public static ClientClass client;
    }
}
