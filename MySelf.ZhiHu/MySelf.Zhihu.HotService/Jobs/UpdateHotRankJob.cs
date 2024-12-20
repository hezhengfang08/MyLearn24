using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
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
    HotRankManager hotRankManager,
    IDataQueryService queryService) : IJob
    {
        public static readonly JobKey Key = new(nameof(UpdateHotRankJob), nameof(HotService));

        public async Task Execute(IJobExecutionContext context)
        {
            var questionStats = questionStatManager.GetAndReset();
            if (questionStats == null) return;

            await hotRankManager.UpdateHotRankAsync(questionStats);

            var hotlist = await hotRankManager.GetTopHotRankAsync();
            var ids = hotlist.Select(hot => hot.Id).ToArray();

            var query = queryService.Questions
                .Where(question => ids.Contains(question.Id))
                .Select(question => new
                {
                    question.Id,
                    question.Title,
                    question.Summary
                });

            var questionLists = await queryService.ToListAsync(query);
            var questionDict = questionLists.ToDictionary(item => item.Id);

            foreach (var hot in hotlist)
            {
                hot.Title = questionDict[hot.Id].Title;
                hot.Summary = questionDict[hot.Id].Summary;
            }

            await hotRankManager.UpdateHotListAsync(hotlist);
        }

    }

}
