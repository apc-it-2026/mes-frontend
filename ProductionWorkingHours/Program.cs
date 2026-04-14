using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductionWorkingHours
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
            Client.UserToken = "2187e642-ef19-4af4-b699-7ca655220c28";//
            Client.Language = "en";
            ProductionWorkingHours frm = new ProductionWorkingHours();
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
