using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SFC_Shift_Setting
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
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "b8dc3efa-0a06-44cd-a47d-56d45bdf0c48";//
            client.Language = "en";
            Application.Run(new F_SFC_Shift_Setting());
        }

        public static SJeMES_Framework.Class.ClientClass client;
    }
}
