using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Queries
{
    [Authorize]
    public record GetQuestionQuery(int Id) : IQuery<Result<QuestionDto>>;
    public class GetQuestionQueryHandler(IDataQueryService dataQueryService) : IQueryHandler<GetQuestionQuery, Result<QuestionDto>>
    {
        public async Task<Result<QuestionDto>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var queryable = dataQueryService.Questions
                .Where(q => q.Id == request.Id)
                .Select(q => new QuestionDto
                {
                    Id = q.Id,
                    Title = q.Title,
                    Description = q.Description,
                    AnswerCount = q.Answers.Count,
                    FollowerCount = q.FollowerCount,
                    ViewCount = q.ViewCount
                });

            var questionDto = await dataQueryService.FirstOrDefaultAsync(queryable);

            return questionDto == null ? Result.NotFound("问题不存在") : Result.Success(questionDto);
        }
    }
}
