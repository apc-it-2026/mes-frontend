using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TierAudits_New
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
           // Client.APIURL = "http://10.3.0.24:8082//api/commoncall";
            Client.UserToken = "f9b41c6a-d475-490a-b4b6-4ad5a7f19605";
            Client.Language = "en";
             Application.Run(new Tier_visual());
            



        }
        public static SJeMES_Framework.Class.ClientClass Client;

    }
}
