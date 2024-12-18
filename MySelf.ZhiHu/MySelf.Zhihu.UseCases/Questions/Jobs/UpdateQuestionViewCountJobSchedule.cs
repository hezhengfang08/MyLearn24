using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Jobs
{
    public static class UpdateQuestionViewCountJobSchedule
    {
        private static readonly TriggerKey Key = new(nameof(UpdateQuestionViewCountJobSchedule), nameof(Questions));

        public static void CreateUpdateQuestionViewCountJobSchedule(
            this IServiceCollectionQuartzConfigurator configurator,
            int intervalTime = 20)
        {
            configurator.AddJob<UpdateQuestionViewCountJob>(triggerConfigurator => triggerConfigurator
                .WithIdentity(UpdateQuestionViewCountJob.Key));

            configurator.AddTrigger(trigger => trigger
                .WithIdentity(Key)
                .ForJob(UpdateQuestionViewCountJob.Key)
                .StartAt(DateBuilder.FutureDate(intervalTime, IntervalUnit.Second))
                .WithSimpleSchedule(builder => builder
                    .RepeatForever()
                    .WithInterval(TimeSpan.FromSeconds(intervalTime))
                ));

        }
    }

}
