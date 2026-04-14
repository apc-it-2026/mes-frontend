using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PO_Wise_Material_Inspection_Results
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            Client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9"; //real library 938657
            Client.Language = "en";
            Application.Run(new CRD_PO_List());
        }
        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
