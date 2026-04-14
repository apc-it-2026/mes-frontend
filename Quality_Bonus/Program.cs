using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quality_Bonus
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
            client = new SJeMES_Framework.Class.ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall";
            // client.UploadUrl = "http://10.3.0.24:8011/api/commoncall";
            client.PicUrl = "http://10.3.0.24:8011";
            //client.APIURL = "http://10.3.0.24:8082//api/commoncall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            client.UserToken = "a6d53c55-a74a-419e-a262-3316f98be956"; //real library 938657
            //client.UserToken = "d8accd8a-c4fa-4516-9fb8-b52c2af86d45"; //real library 67143
            //client.UserToken = "770b299f-225b-488f-aa06-bf51aee775f8"; //real library 938657
            client.Language = "en";
            Application.Run(new QualityBonus());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
