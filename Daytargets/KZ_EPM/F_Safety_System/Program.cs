using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_Safety_System
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
            Client.UserToken = "7fa90c7c-ec06-4a3e-94c3-173be33a66d7";//
            Client.Language = "en";
            Application.Run(new Safety_System());
        }
        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
