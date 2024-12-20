using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using MySelf.Zhihu.UseCases.Questions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Answers.Queries
{
    [Authorize]
    public record GetAnswerWithQuestionQuery(int QuestionId, int AnswerId) : IQuery<Result<AnswerWithQuestionDto>>;
    public class GetAnswerWithQuestionQueryValidator : AbstractValidator<GetAnswerWithQuestionQuery>
    {
        public GetAnswerWithQuestionQueryValidator()
        {
            RuleFor(command => command.QuestionId)
                .GreaterThan(0);

            RuleFor(command => command.AnswerId)
                .GreaterThan(0);
        }
    }
    public class GetAnswerQueryHandler(IDataQueryService dataQueryService)
    : IQueryHandler<GetAnswerWithQuestionQuery, Result<AnswerWithQuestionDto>>
    {
        public async Task<Result<AnswerWithQuestionDto>> Handle(GetAnswerWithQuestionQuery request,
            CancellationToken cancellationToken)
        {
            var answers = dataQueryService.Answers;
            var appUsers = dataQueryService.AppUsers;
            var questions = dataQueryService.Questions;

            var queryable = from answer in answers
                            join question in questions on answer.QuestionId equals question.Id
                            join user in appUsers on answer.CreatedBy equals user.Id
                            where answer.Id == request.AnswerId && answer.QuestionId == request.QuestionId
                            select new AnswerWithQuestionDto
                            {
                                Answer = new AnswerDto
                                {
                                    Id = answer.Id,
                                    Content = answer.Content,
                                    LikeCount = answer.LikeCount,
                                    LastModifiedAt = answer.LastModifiedAt,
                                    CreatedBy = answer.CreatedBy,
                                    CreatedByNickName = user.Nickname,
                                    CreatedByBio = user.Bio
                                },
                                Question = new QuestionDto
                                {
                                    Id = question.Id,
                                    Title = question.Title,
                                    Description = question.Description,
                                    AnswerCount = question.Answers.Count,
                                    FollowerCount = question.FollowerCount,
                                    ViewCount = question.ViewCount
                                }
                            };

            var answerDto = await dataQueryService.FirstOrDefaultAsync(queryable);

            return answerDto == null ? Result.NotFound("回答不存在") : Result.Success(answerDto);
        }
    }

}
