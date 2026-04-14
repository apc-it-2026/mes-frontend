
using MaterialSkin;
using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPIINPUT
{
    static class Program
    {
        

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        //static void Main()
        //{
        //    try
        //    {
        //        configstring = SJeMES_Framework.Common.TXTHelper.ReadToEnd("Config.json");

        //        Dictionary<string, string> Pconfig = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, string>>(configstring);

        //        client.Language = string.Empty;
        //        client.CompanyCode = string.Empty;
        //        client.APIURL = Pconfig["api"];
        //        client.UserCode = string.Empty;
        //        //Client.UploadUrl = Pconfig["uploadurl"];
        //        //Client.PicUrl = Client.UploadUrl.ToLower().Replace("/api/commoncall", "");
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
        //public static SJeMES_Framework.Class.ClientClass client = new SJeMES_Framework.Class.ClientClass();
        //public static MaterialSkinManager.Themes SkinThemes = MaterialSkinManager.Themes.LIGHT;


        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "37645485-8c4c-4d8f-bb83-ea428f2d042e";
            client.Language = "en";
            Application.Run(new Haulting());
        }
        public static ClientClass client;

    }
}
