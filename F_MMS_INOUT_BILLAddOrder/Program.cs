using System;
using System.Windows.Forms;
using SJeMES_Framework.Class;

namespace F_MMS_InOut_BillAddOrder
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
           // client.APIURL = "http://10.2.171.110:80/api/CommonCall";
            //client.UserToken = "e6b5e4fe-11f3-4feb-adc9-f34a8ac12ef5";//cs 14785236987
            client.UserToken = "48b880f4-2685-413d-9bd9-f619089be611";
            client.Language = "en";//
            Application.Run(new InOut_BillOrder());
        }
        public static ClientClass client;
    }
}
