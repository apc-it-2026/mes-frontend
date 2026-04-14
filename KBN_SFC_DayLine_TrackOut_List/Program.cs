using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KBN_SFC_DayLine_TrackOut_List
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            //Client.APIURL = "http://10.2.1.46:8090/api/CommonCall";
            Client.UserToken = "9ae9a211-7cdd-4c74-abe4-d24d991183a9";//
                                                                      // Client.UserToken = "dac7074c-2e56-4606-b916-f77f30789f7e";//
            Client.Language = "en";
            KBN_SFC_DayLine_TrackOut_List frm = new KBN_SFC_DayLine_TrackOut_List();
            frm.StartPosition = FormStartPosition.CenterScreen;
            frm.Show();
            Application.Run(frm);
        }

        public static SJeMES_Framework.Class.ClientClass Client;
    }
}
