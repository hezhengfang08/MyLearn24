using MySelf.Zhihu.Core.QuestionAggregate.Events;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Questions.Queries
{
    [Authorize]
    public record GetQuestionQuery(int Id) : IQuery<Result<QuestionDto>>;
    public class GetQuestionQueryValidator : AbstractValidator<GetQuestionQuery>
    {
        public GetQuestionQueryValidator()
        {
            RuleFor(command => command.Id)
                .GreaterThan(0);
        }
    }
    public class GetQuestionQueryHandler(
        IDataQueryService dataQueryService,
        ICacheService<QuestionDto> cacheService,
        IPublisher publisher) : IQueryHandler<GetQuestionQuery, Result<QuestionDto>>
    {
        public async Task<Result<QuestionDto>> Handle(GetQuestionQuery request, CancellationToken cancellationToken)
        {
            var questionDto = await cacheService.GetOrSetByIdAsync(request.Id, async _ =>
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
                return await dataQueryService.FirstOrDefaultAsync(queryable);
            });

            if (questionDto == null) return Result.NotFound("问题不存在");

            await publisher.Publish(new QuestionViewedEvent(questionDto.Id), cancellationToken);

            return Result.Success(questionDto);
        }
    }
}

