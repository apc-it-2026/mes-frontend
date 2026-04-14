using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Production_Kanban
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //Client.APIURL = "http://10.2.1.50:80/api/CommonCall";
            //   Client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            // Client.APIURL = "http://10.30.1.191:8082//api/CommonCall";
            // Client.UserToken = "39c69540-4fff-4ca4-9088-42507391f32c";//
            Client.UserToken = "337e5f84-2c23-4047-990b-c6eb03b5e815";//
            //Client.UserToken = "dac7074c-2e56-4606-b916-f77f30789f7e";//
            Client.Language = "en";
            Production_KanbanForm frm = new Production_KanbanForm();
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
