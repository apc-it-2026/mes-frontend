using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_BarcodePrinting_WakeOrder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
           // client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            client.UserToken = "47570f94-ecea-4901-98ca-126594c05a11";//
            client.Language = "en";//
            Application.Run(new F_BarcodePrinting_WakeOrderForm());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
