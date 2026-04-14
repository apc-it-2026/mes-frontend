using System.ServiceProcess;
using SJeMES_Framework.Class;

namespace WorkingHoursService
{
    static class Program
    {
        public static ClientClass client;

        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        static void Main(string[] args)
        {
            //if (args.Length > 0 && args[0] == "s") //这里要去除掉，因为Windows控制台引用程序设置启动是否读取不到，使用是另外一个程序设计的话不一样
            //{
            client = new ClientClass();
#if DEBUG
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//开发服务器
            //client.APIURL = "http://localhost:60626/api/CommonCall"; //本地服务器，本地API服务器地址一定不能是正式服务器，不然容易出错。
            //client.APIURL = "http://10.2.171.110:80/api/CommonCall";//测试服务器
            //client.UserToken = "c59f1295-4afa-4d99-980a-b7478a1951d9"; //设置成开发库的token,并且是机器人工时账号123456,密码：mes2021

            //APC
            client.APIURL = "http://10.3.0.24:8082/api/CommonCall";//APC Formal Server
            client.UserToken = "67b6089d-ef85-41a2-a81d-0afb9732d18e"; //APC Account number：123456，Password：mes2022

            //APH
            //client.APIURL = "http://10.30.1.191:8083/api/CommonCall";//APH测试服务器
            //client.UserToken = "89535bcb-36ac-4fdc-b7fb-321e60058fba"; //APH账号：123456，密码：2021mes
#else
            //一般时候不需要在这里调试，如果特殊情况，才会使用。一般不需要动这些代码。
            client.APIURL = "http://10.2.1.46:8090/api/CommonCall";//正式服务器
            client.UserToken = "06e218e8-9810-4aee-bc6a-a8514387bcbb"; //设置成正式库的token,并且是机器人工时账号123456，密码：2021mes
#endif
            client.Language = "en";

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                new WorkingHoursService()
            };
            ServiceBase.Run(ServicesToRun);
            //}
            //else
            //{
            //    Console.WriteLine("这是Windows应用程序");
            //    Console.WriteLine("请选择，[1]安装服务 [2]卸载服务 [3]启动服务 [4]停止服务 [5]退出");
            //    string inputCmd = Console.ReadLine();
            //    if (inputCmd != null)
            //    {
            //        ServiceController sc = new ServiceController("WorkingHoursService");
            //        var rs = int.Parse(inputCmd);
            //        switch (rs)
            //        {
            //            case 1:
            //                //取当前可执行文件路径，加上"s"参数，证明是从windows服务启动该程序
            //                var path = Process.GetCurrentProcess().MainModule.FileName + " s";
            //                Process.Start("sc", "create WorkingHoursService binpath= \"" + path + "\" displayName= WorkingHoursService start= auto");
            //                Console.WriteLine("安装成功");
            //                Console.Read();
            //                break;
            //            case 2:
            //                Process.Start("sc", "delete WorkingHoursService");
            //                Console.WriteLine("卸载成功");
            //                Console.Read();
            //                break;
            //            case 3:
            //                if (sc.Status== ServiceControllerStatus.Stopped||sc.Status== ServiceControllerStatus.StopPending)
            //                {
            //                    sc.Start();
            //                    sc.Refresh();
            //                }
            //                Console.WriteLine("启动服务成功");
            //                Console.Read();
            //                break;
            //            case 4:
            //                if (sc.Status == ServiceControllerStatus.Running)
            //                {
            //                    sc.Stop();
            //                    sc.Refresh();
            //                }
            //                Console.WriteLine("停止服务成功");
            //                Console.Read();
            //                break;
            //            case 5: break;
            //        }
            //    }
            //}
        }
    }
}