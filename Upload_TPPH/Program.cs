using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Upload_TPPH
{
    internal static class Program
    {
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
            client.UploadUrl = "http://10.3.0.24:8011/api/commoncall";
            client.PicUrl = "http://10.3.0.24:8011";
            //client.APIURL = "http://10.3.0.24:8082//api/commoncall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            client.UserToken = "b0062c66-d559-437d-b0c6-ef0497a6cff0"; //Real library 938657
            client.Language = "en";
            Application.Run(new Upload_TPPH());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
