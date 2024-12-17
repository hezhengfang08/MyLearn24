using MySelf.Zhihu.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Queries
{
    [Authorize]
    public record GetAnswersQuery(int QuestionId, Pagination Pagination) : IQuery<Result<PagedList<AnswerDto>>>;
    public class GetQuestionQueryValidator : AbstractValidator<GetAnswersQuery>
    {
        public GetQuestionQueryValidator()
        {
            RuleFor(command => command.QuestionId)
                .GreaterThan(0);
        }
    }


    public class GetAnswersQueryHandler(IDataQueryService dataQueryService) : IQueryHandler<GetAnswersQuery, Result<PagedList<AnswerDto>>>
    {
        public async Task<Result<PagedList<AnswerDto>>> Handle(GetAnswersQuery request, CancellationToken cancellationToken)
        {
            var queryable = from answer in dataQueryService.Answers
                            join user in dataQueryService.AppUsers on answer.CreatedBy equals user.Id
                            where answer.QuestionId == request.QuestionId
                            orderby answer.LikeCount descending
                            select new AnswerDto
                            {
                                Id = answer.Id,
                                Content = answer.Content,
                                LikeCount = answer.LikeCount,
                                LastModifiedAt = answer.LastModifiedAt,
                                CreatedBy = answer.CreatedBy,
                                CreatedByNickName = user.Nickname,
                                CreatedByBio = user.Bio
                            };

            var answers = await dataQueryService.ToPageListAsync(queryable, request.Pagination);

            return answers.Count == 0 ? Result.NotFound("回答不存在") : Result.Success(answers);
        }
    }

}
