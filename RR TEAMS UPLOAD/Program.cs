using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RR_TEAMS_UPLOAD
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
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "866b0315-b634-4edf-a8aa-8e2438fea041";
            client.Language = "en";
            Application.Run(new Form1());
        }
        public static ClientClass client;
    }
}
