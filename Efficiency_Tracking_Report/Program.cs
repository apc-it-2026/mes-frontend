using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Efficiency_Tracking_Report
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
            Client.UserToken = "6b3c5e42-f85c-4878-b021-971a0cdb366e";//
            Client.Language = "en";
            Application.Run(new Efficiency_Tracking_Report());
        }
        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
