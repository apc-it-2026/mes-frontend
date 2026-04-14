using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hourly_Output_Management
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
            //Client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            Client.UserToken = "99560c54-9b3e-41f5-a735-3ba1cb376b0c";//
            Client.Language = "en";
            Hourly_management frm = new Hourly_management();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            Application.Run(frm);
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
