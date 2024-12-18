using MySelf.Zhihu.SharedKernel.Result;
using MySelf.Zhihu.UseCases.Contracts.Common.Interfaces;


namespace MySelf.Zhihu.HotService.Data
{
    public class QuestionStatQuery(IDataQueryService queryService)
    {
        public async Task<Result<Dictionary<int, QuestionStat>>> GetLatest()
        {
            var createdAtBegin = DateTimeOffset.Now.AddDays(-7);
            var lastModifiedBegin = DateTimeOffset.Now.AddHours(-48);

            var questions = queryService.Questions;
            var answers = queryService.Answers;

            var queryable = from question in questions
                            join answer in answers on question.Id equals answer.QuestionId
                            group answer by new
                            {
                                question.Id,
                                question.ViewCount,
                                question.FollowerCount,
                                question.LastModifiedAt,
                                question.CreatedAt
                            } into g
                            where g.Key.LastModifiedAt >= lastModifiedBegin
                                  && g.Key.CreatedAt >= createdAtBegin
                            orderby g.Key.ViewCount descending
                            select new
                            {
                                Id = g.Key.Id,
                                ViewCount = g.Key.ViewCount,
                                FollowCount = g.Key.FollowerCount,
                                AnswerCount = g.Count(),
                                LikeCount = g.Sum(answer => answer.LikeCount)
                            };
            var questionList = await queryService.ToListAsync(queryable);
            if (questionList.Count == 0) return Result.NotFound();

            var questionStats = questionList.ToDictionary(item => item.Id, item => new QuestionStat
            {
                ViewCount = item.ViewCount,
                FollowCount = item.FollowCount,
                AnswerCount = item.AnswerCount,
                LikeCount = item.LikeCount
            });

            return Result.Success(questionStats);
        }
    }
}
