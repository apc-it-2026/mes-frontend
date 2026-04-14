using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Device_Standing_Book
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
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.UserToken = "cbdd90af-a7dc-4658-b672-b2c926d12a27";//
            Client.Language = "en";
            Application.Run(new Device_Standing_BookMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
