using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_SFC_TrackOut_AssemblyOrder
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
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //Client.APIURL = "http://10.3.0.29:8092//api/CommonCall";
            //Client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            Client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9";//
            Client.Language = "en";
            Application.Run(new F_SFC_TrackOut_AssemblyOrder());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
