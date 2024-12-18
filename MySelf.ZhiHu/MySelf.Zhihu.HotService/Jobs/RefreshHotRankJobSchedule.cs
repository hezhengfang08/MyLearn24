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
            this IServiceCollectionQuartzConfigurator configurator,
            int hour = 5)
        {
            configurator.AddJob<RefreshHotRankJob>(triggerConfigurator => triggerConfigurator
                .WithIdentity(RefreshHotRankJob.Key)
                .RequestRecovery());

            configurator.AddTrigger(trigger => trigger
                .WithIdentity(Key)
                .ForJob(RefreshHotRankJob.Key)
                .WithCronSchedule($"0 0 {hour} * * ?"));

            configurator.AddTrigger(trigger => trigger
                .ForJob(RefreshHotRankJob.Key)
                .StartNow());
        }
    }

}
