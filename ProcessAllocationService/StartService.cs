using System.Threading.Tasks;
using Quartz;
using Quartz.Impl;

namespace ProcessAllocationService
{
    class StartService
    {
        private Task<IScheduler> scheduler;

        public void Start()
        {
            // 开启调度
            ISchedulerFactory sf = new StdSchedulerFactory();
            scheduler = sf.GetScheduler();
            //IJobDetail job = JobBuilder.Create<DemoJob>().Build();
            IJobDetail job2 = JobBuilder.Create<ProcessAllocationJob>().Build();
            // 服务启动时执行一次
            // ITrigger triggerNow = TriggerBuilder.Create().StartNow().Build();

            //// 每日22点执行一次
            //ITrigger trigger = TriggerBuilder.Create().StartNow().WithCronSchedule("0 0 22 1/1 * ? ").Build();

            // 每1分钟执行一次
            ITrigger trigger = TriggerBuilder.Create().StartNow().WithCronSchedule("0 0/1 * * * ? ").Build();
            //scheduler.Result.ScheduleJob(job, trigger);
            scheduler.Result.ScheduleJob(job2, trigger);
            scheduler.Result.Start(); //启动quartz服务
            LogHelper.Info("Processing and production allocation timing service begins。");
        }

        public void Stop()
        {
            scheduler.Result.Shutdown(false); //等待正在运行的计划任务执行完毕；false：强制关闭
        }
    }
}