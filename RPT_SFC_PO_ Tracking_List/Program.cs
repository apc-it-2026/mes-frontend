using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPT_SFC_PO_Tracking_List
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
            //client.APIURL = "http://10.3.0.24:8082/api/CommonCall";
          // client.UserToken = "dac7074c-2e56-4606-b916-f77f30789f7e";//
            client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9";//
           // client.UserToken = "3b1565ae-0e41-4bec-ba20-319f42e7b629";//
            client.Language = "en";
            Application.Run(new RPT_SFC_PO_Tracking_List());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}
