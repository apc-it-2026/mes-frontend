using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Data_Lock
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
            client.UploadUrl = "http://10.3.0.24:8011/api/commoncall";
            client.PicUrl = "http://10.3.0.24:8011";
            //client.APIURL = "http://10.3.0.24:8082//api/commoncall";
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            //client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9"; //Real library 938657
            client.Language = "en";
            Application.Run(new Data_Lock());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
