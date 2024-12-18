using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;
using Quartz;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Jobs
{
    public class RefreshHotRankJob(ISchedulerFactory schedulerFactory, QuestionStatQuery questionStatQuery, QuestionStatManager questionStatManager, HotRankManager hotRankManager) : IJob
    {
        public static readonly JobKey Key = new(nameof(RefreshHotRankJob), nameof(HotService));
        public async Task Execute(IJobExecutionContext context)
        {
            var result = await questionStatQuery.GetLatest();
            if (!result.IsSuccess) return;

            var questionStats = result.Value!;

            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.PauseTrigger(UpdateHotRankJobSchedule.Key);

            await hotRankManager.ClearHotRankAsync();
            await hotRankManager.CreateHotRankAsync(questionStats);

            questionStatManager.Set(questionStats);

            await scheduler.ResumeTrigger(UpdateHotRankJobSchedule.Key);
        }
    }
}
