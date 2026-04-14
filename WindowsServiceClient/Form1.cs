using System;
using System.Collections;
using System.Configuration.Install;
using System.ServiceProcess;
using System.Windows.Forms;

namespace WindowsServiceClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string serviceFilePath = $"{Application.StartupPath}\\WorkingHoursService.exe";
        string serviceName = "WorkingHoursService";


        //判断服务是否存在
        private bool IsServiceExisted(string serviceName)
        {
            ServiceController[] services = ServiceController.GetServices();
            foreach (ServiceController sc in services)
            {
                if (sc.ServiceName.ToLower() == serviceName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        //安装服务
        private void InstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                IDictionary savedState = new Hashtable();
                installer.Install(savedState);
                installer.Commit(savedState);
            }
        }

        //卸载服务
        private void UninstallService(string serviceFilePath)
        {
            using (AssemblyInstaller installer = new AssemblyInstaller())
            {
                installer.UseNewContext = true;
                installer.Path = serviceFilePath;
                installer.Uninstall(null);
            }
        }

        //启动服务
        private void ServiceStart(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Stopped)
                {
                    control.Start();
                    control.Refresh();
                }
            }
        }

        //停止服务
        private void ServiceStop(string serviceName)
        {
            using (ServiceController control = new ServiceController(serviceName))
            {
                if (control.Status == ServiceControllerStatus.Running)
                {
                    control.Stop();
                    control.Refresh();
                }
            }
        }

        //事件：安装服务
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (IsServiceExisted(serviceName)) UninstallService(serviceFilePath);
            InstallService(serviceFilePath);
            MessageBox.Show("安装成功");
        }

        //事件：启动服务
        private void btnStart_Click(object sender, EventArgs e)
        {
            if (IsServiceExisted(serviceName)) ServiceStart(serviceName);
            MessageBox.Show("启动成功");
        }

        //事件：停止服务
        private void btnStop_Click(object sender, EventArgs e)
        {
            if (IsServiceExisted(serviceName)) ServiceStop(serviceName);
            MessageBox.Show("停止成功");
        }

        //事件：卸载服务
        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (IsServiceExisted(serviceName))
            {
                ServiceStop(serviceName);
                UninstallService(serviceFilePath);
                MessageBox.Show("卸载服务成功");
            }
        }
    }
}