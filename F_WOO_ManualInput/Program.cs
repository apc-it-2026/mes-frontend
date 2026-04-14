using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_WOO_ManualInput
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
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
           // client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //client.UserToken = "54b4ebdd-d7a5-4495-999d-a7e3ee8631c0";//
            client.UserToken= "d09d7b0a-64ba-4483-9461-41c21e738c49";
            client.Language = "cn";//
            Application.Run(new F_WOO_ManualInput());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
