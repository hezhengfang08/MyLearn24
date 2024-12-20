using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;
using Quartz;
using Quartz.Impl.Matchers;
using Quartz.Spi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Jobs
{
    public class RefreshHotRankJob( QuestionStatQuery questionStatQuery, QuestionStatManager questionStatManager, HotRankManager hotRankManager) : IJob
    {
        public static readonly JobKey Key = new(nameof(RefreshHotRankJob), nameof(HotService));
        public async Task Execute(IJobExecutionContext context)
        {
            var result = await questionStatQuery.GetLatest();
            if (!result.IsSuccess) return;

            var questionStats = result.Value!;

            var triggerKey = GroupMatcher<TriggerKey>.GroupEquals(nameof(UpdateHotRankJob));
            await context.Scheduler.PauseTriggers(triggerKey);

            await hotRankManager.ClearHotRankAsync();
            await hotRankManager.CreateHotRankAsync(questionStats);

            questionStatManager.Set(questionStats);

            await context.Scheduler.ResumeTriggers(triggerKey);
        }
    }
}
