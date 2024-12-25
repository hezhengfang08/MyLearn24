using MySelf.Zhihu.HotService.Core;
using MySelf.Zhihu.HotService.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Queries
{
    public record GetQuestionHotListQuery : IQuery<Result<List<QuestionHotList>>>;

    public class GetQuestionHotListQueryHandler(
        HotRankManager hotRankManager) : IQueryHandler<GetQuestionHotListQuery, Result<List<QuestionHotList>>>
    {
        public async Task<Result<List<QuestionHotList>>> Handle(GetQuestionHotListQuery request, CancellationToken cancellationToken)
        {
            return await hotRankManager.GetHotListAsync();
        }
    }
}
