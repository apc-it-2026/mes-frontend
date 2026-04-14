using System;
using System.IO;
using log4net.Config;
using SJeMES_Framework.Class;
using Topshelf;

namespace ProcessAllocationService
{
    class Program
    {
        public static ClientClass client;

        static void Main(string[] args)
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase;
            FileInfo fileInfo = new FileInfo(path);
            XmlConfigurator.Configure(fileInfo);
            client = new ClientClass();
            //client.APIURL = "http://10.2.1.46:8090/api/CommonCall"; //正式服务器
            //client.APIURL = "http://10.2.1.50:80/api/CommonCall";//开发服务器
            //client.APIURL = "http://10.2.171.110:80/api/CommonCall";//测试服务器
            //client.APIURL = "http://localhost:60626/api/CommonCall";
            //client.UserToken = "1269fe54-91fe-4e55-8024-919647d62f1a";//开发库账号，这个是加工生产调拨定时服务专有账号100001号
            //client.UserToken = "3ab382ee-25b9-4fcb-8845-84b697d0da4f"; //正式库账号，这个是加工生产调拨定时服务专有账号100001号，密码：mes2021

            //APC
             client.APIURL = "http://10.3.0.24:8082/api/CommonCall";//APC Formal Server
            //client.APIURL = "http://localhost:60626/api/CommonCall";//
            client.UserToken = "40b8ac56-ec29-4d11-aae7-76c9bde8b863"; //APC Formal library account：100001，password: 100001


            client.Language = "en";

            string serviceName = "ProcessAllocationService";
            HostFactory.Run(config =>
            {
                config.Service<StartService>(s =>
                {
                    s.ConstructUsing(name => new StartService());
                    s.WhenStarted(service => service.Start());
                    s.WhenStopped(service => service.Stop());
                });

                config.RunAsLocalSystem(); //需要以“本地系统”权限启动服务，否则会报http无法注册
                config.SetDescription("Processing and production allocation timing service");
                config.SetServiceName(serviceName);
                config.SetDisplayName(serviceName);
            });
        }
    }
}