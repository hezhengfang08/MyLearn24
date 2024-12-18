using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Jobs
{
    public static class UpdateHotRankJobSchedule
    {
        public static readonly TriggerKey Key = new(nameof(UpdateHotRankJobSchedule), nameof(HotService));

        public static void CreateUpdateHotRankJobSchedule(
            this IServiceCollectionQuartzConfigurator configurator,
            int intervalTime = 1)
        {
            configurator.AddJob<UpdateHotRankJob>(triggerConfigurator => triggerConfigurator
                .WithIdentity(UpdateHotRankJob.Key));

            configurator.AddTrigger(trigger => trigger
                .WithIdentity(Key)
                .ForJob(UpdateHotRankJob.Key)
                .StartAt(DateBuilder.FutureDate(intervalTime, IntervalUnit.Minute))
                .WithSimpleSchedule(builder => builder
                    .RepeatForever()
                    .WithInterval(TimeSpan.FromMinutes(intervalTime))
                ));
        }
    }

}
