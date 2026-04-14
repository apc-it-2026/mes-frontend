using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StitchingInput_Domestic
{
    static class Program
    {
        /// <summary>
        ///     The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            client.APIURL = "http://localhost:60626/api/CommonCall"; //local interface
            client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9"; // Development library users
            //client.UserToken = "6508d475-9a1d-421a-8d3d-8fb0375aa259"; // Development library users
            //client.UserToken = "4e0c872a-e6e3-4dcd-880a-30fce17c958b"; // Development library users
            client.Language = "en";
            client.WebServiceUrl = "http://10.3.0.24:8081/SJ-WebService.asmx";
            Application.Run(new StitchingInput_Domestic());
        }

        public static ClientClass client;

    }
}
