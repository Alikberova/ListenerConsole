using slsr.Jobs;
using Quartz;
using Quartz.Impl;
using System;
using System.Threading.Tasks;

namespace slsr
{
    public class Scheduler
    {
        public async Task<JobKey> ScheduleJob<T>(DateTimeOffset? startAtTime = null, JobKey? vncCheckerJobKey = null) 
            where T : class, IJob
        {
            IScheduler scheduler = await StdSchedulerFactory.GetDefaultScheduler();
            await scheduler.Start();

            IJobDetail jobDetail = JobBuilder.Create<T>().Build();

            ITrigger trigger;
            TriggerBuilder triggerBuilder = TriggerBuilder.Create().StartNow()
                .WithSimpleSchedule(x => x.WithIntervalInSeconds(Constants.JobEecutionIntervalSec)
                .RepeatForever());

            if (vncCheckerJobKey != null)
            {
                triggerBuilder = triggerBuilder.UsingJobData(nameof(Checker), vncCheckerJobKey.Name);
            }

            if (startAtTime != null)
            {
                trigger = triggerBuilder.StartAt((DateTimeOffset)startAtTime).Build();
            }
            else
            {
                trigger = triggerBuilder.StartNow().Build();
            }

            await scheduler.ScheduleJob(jobDetail, trigger);

            return jobDetail.Key;
        }
    }
}
