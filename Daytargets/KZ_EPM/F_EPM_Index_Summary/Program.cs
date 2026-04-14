using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Index_Summary
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
            Client.UserToken = "b0581fc2-b3a2-48bf-bac9-ffd1b1801218";//
            Client.Language = "en";
            Application.Run(new Index_SummaryMainForm());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
