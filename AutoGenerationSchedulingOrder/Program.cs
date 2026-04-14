using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoGenerationSchedulingOrder
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
           // client.APIURL = "http://localhost:60626/api/CommonCall";
            client.APIURL = "http://10.3.0.29:8092/api/CommonCall";//APC Integration Test Library
            //client.APIURL = "http://10.3.0.24:8082/api/CommonCall";//APC production Library
            client.UserToken = "988b291d-9840-40a6-a007-df4616abb388";  // UserId:111222333  password: mes2022 production Library
            //client.UserToken = "af00aede-2c07-47b2-a30d-62d75312fa69";//Debug

            client.Language = "en";
            Application.Run(new AutoGenerationSchedulingOrderForm());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}
