using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.HotService.Jobs
{
    public class UpdateHotRankJob(
    QuestionStatManager questionStatManager,
    HotRankManager hotRankManager) : IJob
    {
        public static readonly JobKey Key = new(nameof(UpdateHotRankJob), nameof(HotService));

        public async Task Execute(IJobExecutionContext context)
        {
            var questionStats = questionStatManager.GetAndReset();
            if (questionStats == null) return;

            await hotRankManager.UpdateHotRankAsync(questionStats);
        }

    }

}
