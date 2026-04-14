using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RPT_SFC_SuspendedWorkingHours
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
            //Client.APIURL = "http://10.30.1.191:8082//api/CommonCall";
            Client.UserToken = "669a1d76-7862-495c-8baf-2ab66b9b04ea";
            Client.Language = "en";
            SetUpQuery frm = new SetUpQuery();
            if (Screen.AllScreens.Count() != 1)
            {
                for (int i = 0; i < Screen.AllScreens.Count(); i++)
                {
                    Screen s = Screen.AllScreens[i];
                    if (!s.Primary)
                    {
                        frm.Location = new System.Drawing.Point(s.Bounds.X, s.Bounds.Y);
                        frm.Size = new System.Drawing.Size(Screen.AllScreens[i].Bounds.Width, Screen.AllScreens[i].Bounds.Height);
                    }
                }
            }
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            Application.Run(frm);
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}