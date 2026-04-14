using SJeMES_Control_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingIEAchievementInput
{
    internal static class Program
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
            //client.APIURL = "http://10.2.171.110:2222/api/CommonCall";//APE测试库
            client.UserToken = "cc4ea3f8-d43a-42d1-8e9c-f5710fac74e0";
            client.Language = "en";
            Application.Run(new IERateForm());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
