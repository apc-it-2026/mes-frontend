using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TPM_KanBan001
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
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.UserToken = "1af5d4b8-9e64-4e8b-bf0a-19c770ecba16";//
            Client.Language = "en";
            Application.Run(new mainform());
        }
        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
