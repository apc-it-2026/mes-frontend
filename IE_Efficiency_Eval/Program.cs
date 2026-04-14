using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IE_Efficiency_Eval
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
            client = new SJeMES_Framework.Class.ClientClass();
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            client.APIURL = "http://localhost:60626/api/CommonCall";
            client.UserToken = "b0062c66-d559-437d-b0c6-ef0497a6cff0";//
            client.Language = "en";
            Application.Run(new IE_Efficiency_Evaluation());
        }
        public static SJeMES_Framework.Class.ClientClass client;
    }
}



