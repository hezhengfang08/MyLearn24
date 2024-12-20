using Quartz;


namespace MySelf.Zhihu.HotService.Jobs
{
    public static class UpdateHotRankJobSchedule
    {


        public static void CreateUpdateHotRankJobSchedule(
            this IScheduler scheduler,
            int intervalTime = 60)
        {
            var schedulerId = scheduler.SchedulerInstanceId;
            var triggerKey = new TriggerKey($"{nameof(UpdateHotRankJobSchedule)}-{schedulerId}", nameof(UpdateHotRankJob));

            var jobDetail = JobBuilder.Create<UpdateHotRankJob>()
          .WithIdentity(UpdateHotRankJob.Key)
          .Build();

            var trigger = TriggerBuilder.Create()
           .WithIdentity(triggerKey)
           .ForJob(UpdateHotRankJob.Key)
           .WithSimpleSchedule(builder => builder
               .RepeatForever()
               .WithInterval(TimeSpan.FromSeconds(intervalTime)))
           .Build();

            scheduler.ScheduleJob(jobDetail, trigger);
        }
    }

}
