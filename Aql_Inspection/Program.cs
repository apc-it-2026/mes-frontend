using SJeMES_Framework.Class;


namespace Aql_Inspection
{
    static class Program
    {
        internal static object client;

        // ✅ FIX: Strongly typed Client
        public static SJeMES_Framework.Class.ClientClass Client { get; internal set; }

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ Initialize Client
            Client = new SJeMES_Framework.Class.ClientClass();
            Client.APIURL = "http://localhost:60626/api/CommonCall";
            Client.UserToken = "ee6920c0-3d97-4da8-bed8-1f40088785da";
            Client.Language = "en";


            Application.Run(new AQL_Inspection());

        }
    }
}
