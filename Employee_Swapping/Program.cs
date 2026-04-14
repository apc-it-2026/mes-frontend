using SJeMES_Framework.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Swapping
{
    static class Program
    {

        public static ClientClass client;

        /// <summary>
        ///     The main entry point for the application。
        /// </summary>
        [STAThread]
        static void Main()
        {
           
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            client = new ClientClass();
            //client.APIURL = "http://10.3.0.24:8082/api/CommonCall";//official server
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//Development server, this is only used during development, the official library will read the data in Config.json。
            //client.APIURL = "http://localhost:5000/api/CommonCall";//This is the address where dnSpy starts
            client.APIURL = "http://localhost:60626/api/CommonCall"; //local server
            //client.UserToken = "e1aba529-27e6-46fc-be11-6af90a99e817"; //development library
            //client.UserToken = "03d98c1f-e7eb-429d-af34-beb617362b27";//test library
            //client.UserToken = "d0672ecf-dd98-4c99-9089-34d120843ee3"; //Formal library
            //client.UserToken = "ff9250c4-688f-4622-b5c9-9b7369057cd9"; //real library 938657
            client.UserToken = "988b291d-9840-40a6-a007-df4616abb388"; //test library 938657
            //client.UserToken = "45bb2224-f2c2-4cdd-8b97-ee7a34f54850"; //test library 67470
            //client.UserToken = "99560c54-9b3e-41f5-a735-3ba1cb376b0c"; //Real library 938657
            client.Language = "en";
            Application.Run(new Employee_Swapping());
        }

    }
}
