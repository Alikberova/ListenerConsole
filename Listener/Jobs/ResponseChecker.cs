using Microsoft.Extensions.Logging;
using Quartz;
//using Serilog;
using System;
using System.Threading.Tasks;

namespace slsr.Jobs
{
    public class ResponseChecker : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            JobDataMap jobDataMap = context.Trigger.JobDataMap;
            JobKey vncCheckerJobKey = new JobKey(jobDataMap.GetString(nameof(Checker)));
            Sender sender = new Sender();
            //IJobDetail jobD = await context.Scheduler.GetJobDetail(vncCheckerJobKey);
            //IReadOnlyCollection<IJobExecutionContext> exJobs = await context.Scheduler.GetCurrentlyExecutingJobs();
            //var vncCheckerContext = exJobs.Where(j => j.JobDetail.Key == vncCheckerJobKey).ToList();
            //if (vncCheckerContext.Count > 0)
            //{
            //}

            if (await sender.NeedPause())
            {
                await context.Scheduler.PauseJob(vncCheckerJobKey);
                //await context.Scheduler.PauseTrigger(context.Trigger.Key);
                Console.WriteLine("Paused job " + nameof(Checker));
                Console.WriteLine("Stopped to send messages");
            }
            if (await sender.NeedStart())
            {
                await context.Scheduler.ResumeJob(vncCheckerJobKey);
                Console.WriteLine("Resumed job " + nameof(Checker));
            }
        }
    }
}
