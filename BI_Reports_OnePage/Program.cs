using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using SJeMES_Control_Library;

namespace BI_Reports_OnePage
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

            Client.UserToken = "11a9e011-68b4-40fa-9b86-4e6d3816865e";//

            Client.Language = "en";
            //CustomMessageBox frm = new CustomMessageBox();
            BI_Reports frm = new BI_Reports();
            frm.StartPosition = FormStartPosition.CenterScreen;


            frm.Show();
            Application.Run(frm);
        }

        public static SJeMES_Framework.Class.ClientClass Client;






    }
}
