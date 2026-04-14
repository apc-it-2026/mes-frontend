using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IE_Bonus_Calculation
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
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "b0062c66-d559-437d-b0c6-ef0497a6cff0";//
            client.Language = "en";
            Application.Run(new IE_Bonus_Calculator());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}
