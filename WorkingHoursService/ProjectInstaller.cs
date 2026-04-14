using System.ComponentModel;
using System.Configuration.Install;

namespace WorkingHoursService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
    }
}