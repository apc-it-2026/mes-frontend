using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LIne_Plan_Upload
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
            client.UserToken = "b0062c66-d559-437d-b0c6-ef0497a6cff0";
            client.Language = "en";
            Application.Run(new LIne_Plan_Upload());
        }
        public static ClientClass client;
    }
}
