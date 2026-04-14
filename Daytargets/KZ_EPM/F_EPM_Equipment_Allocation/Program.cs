using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Equipment_Allocation
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
            Client.UserToken = "e0ae5804-65ce-431d-aee0-44cb8f12d9e3";//
            Client.Language = "en";
            Application.Run(new Equipment_AllocationMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
