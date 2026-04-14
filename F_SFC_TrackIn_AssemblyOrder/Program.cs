using System;
using System.Windows.Forms;

namespace F_SFC_TrackIn_AssemblyOrder
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

           // Client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
           //Client.APIURL = "http://10.2.171.110:80/api/CommonCall";
            Client.UserToken = "b3a17f2b-d76c-4ead-b4b9-e8ddec15bfad";
            Client.Language = "en";
            Application.Run(new F_SFC_TrackIn_AssemblyOrder());
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
