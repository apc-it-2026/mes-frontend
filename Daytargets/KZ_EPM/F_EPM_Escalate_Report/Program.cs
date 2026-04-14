using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace F_EPM_Escalate_Report
{
    static class Program
    {
        // <summary>
        // The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.UserToken = "b49db19b-3ab5-4318-b3d4-76a9c3762726";//
            Client.Language = "en";
            Application.Run(new Escalate_Report());
        }

        public static SJeMES_Framework.Class.ClientClass Client;

        //static void Main()
        //{
        //    try
        //    {
        //        configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");
        //        Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);
        //        Client.Language = string.Empty;
        //        Client.CompanyCode = string.Empty;
        //        Client.APIURL = Pconfig["api"];
        //        Client.UserCode = string.Empty;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //    Application.EnableVisualStyles();
        //    Application.SetCompatibleTextRenderingDefault(false);
        //    //Application.Run(new frmLogin());
        //}
        //public static string DefaultPrinter;
        //public static List<SJeMES_Framework.Web.JSONMenu> Menus;
        //public static Dictionary<string, SJeMES_Framework.Web.JSONMenu> MenusInfo;
        //public static string configstring;
        //public static SJeMES_Framework.Class.ClientClass Client = new SJeMES_Framework.Class.ClientClass();
    }
}
