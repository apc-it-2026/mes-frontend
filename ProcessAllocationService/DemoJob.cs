using System;
using System.Threading.Tasks;
using Quartz;

namespace ProcessAllocationService
{
    class DemoJob : IJob
    {
        Task IJob.Execute(IJobExecutionContext context)
        {
            return Task.Factory.StartNew(() => { Console.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")); });
        }
    }
}