using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_WOO_WareHouseReport
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
            //client.APIURL = "http://10.3.0.24:8082/api/CommonCall";
            //client.UserToken = "6550f2a7-d248-4131-a49d-4ed1a71cb116";
            client.UserToken= "78b178ad-3b85-4b0d-8dc2-184b3c96fbcd";
            client.Language = "en";//
            Application.Run(new F_WOO_WareHouseReportForm());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
