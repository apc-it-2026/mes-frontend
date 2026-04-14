using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorkingHoursStandard_VReport
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
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "9d8b98df-93b3-4f8b-82ea-cc74e555cbe7";//
            client.Language = "en";
            Application.Run(new WorkingHoursStandard_VReport());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}
