using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Jobs
{
    public static class RefreshHotRankJobSchedule
    {
        private static readonly TriggerKey Key = new(nameof(RefreshHotRankJobSchedule), nameof(HotService));

        public static void CreateRefreshHotRankJobSchedule(
            this IScheduler scheduler,
        int hour = 5)
        {
            var jobDetail = JobBuilder.Create<RefreshHotRankJob>()
             .WithIdentity(RefreshHotRankJob.Key)
             .Build();

            var triggers = new List<ITrigger>
        {
            TriggerBuilder.Create()
                .WithIdentity(Key)
                .ForJob(RefreshHotRankJob.Key)
                .WithCronSchedule($"0 0 {hour} * * ?")
                .Build(),

            TriggerBuilder.Create()
                .ForJob(RefreshHotRankJob.Key)
                .StartNow()
                .Build(),
        };

            scheduler.ScheduleJob(jobDetail, triggers, true);
        }
    }

}
