using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;
using Topshelf;

namespace ProcessAllocationService
{
    class QuartzServer : ServiceControl, ServiceSuspend
    {
        private readonly Task<IScheduler> scheduler;

        public QuartzServer()
        {
            scheduler = StdSchedulerFactory.GetDefaultScheduler();
        }

        public bool Start(HostControl hostControl)
        {
            scheduler.Result.Start();
            return true;
        }

        public bool Stop(HostControl hostControl)
        {
            scheduler.Result.Clear();
            return true;
        }

        public bool Pause(HostControl hostControl)
        {
            scheduler.Result.ResumeAll();
            return true;
        }

        public bool Continue(HostControl hostControl)
        {
            scheduler.Result.PauseAll();
            return true;
        }
    }
}