using F_TMS_TierMeeting2_Main.Delivery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_TMS_TierMeeting2_Main
{
    static class Program
    {
        public static SJeMES_Framework.Class.ClientClass client;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);


            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "92eca3d8-2e72-45a8-b0fa-8c30693afb85";
            client.UserToken = "6c6a3cd2-1f96-4b69-98f3-3d2d7bfbe7aa";
            client.Language = "en";
            Application.Run(new IntroForm());
         //   Application.Run(new POComRateForm("1P", DateTime.Parse("2022/3/12"),"3"));
           // Application.Run(new MaturityAssessmentForm("1PG1", 2,"1P", "1PG1"));
        }
    }
}
