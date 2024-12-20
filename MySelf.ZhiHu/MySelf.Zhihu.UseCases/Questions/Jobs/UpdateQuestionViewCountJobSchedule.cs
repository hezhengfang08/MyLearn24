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
            this IScheduler scheduler,
            int intervalTime = 20)
        {
            var jobDetail = JobBuilder.Create<UpdateQuestionViewCountJob>()
           .WithIdentity(UpdateQuestionViewCountJob.Key)
           .Build();

            var trigger = TriggerBuilder.Create()
              .WithIdentity(Key)
              .ForJob(UpdateQuestionViewCountJob.Key)
              .StartAt(DateBuilder.FutureDate(intervalTime, IntervalUnit.Second))
              .WithSimpleSchedule(builder => builder
                  .RepeatForever()
                  .WithInterval(TimeSpan.FromSeconds(intervalTime)))
              .Build();

            scheduler.ScheduleJob(jobDetail, trigger);

        }
    }

}
