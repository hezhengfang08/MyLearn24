using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.Interfaces;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Services
{
    public class FollowQuestionService(IReadRepository<Question> questions) : IFollowQuestionService
    {
        public async Task<IResult> FollowAsync(AppUser appuser, int questionId, CancellationToken cancellationToken)
        {
            var question = await questions.GetByIdAsync(questionId, cancellationToken);
            if (question == null) return Result.NotFound("关注问题不存在");

            question.FollowerCount += 1;

            var result = appuser.AddFollowQuestion(questionId);
            return Result.From(result);
        }

        public async Task UnfollowAsync(AppUser appuser, int questionId, CancellationToken cancellationToken)
        {
            var question = await questions.GetByIdAsync(questionId, cancellationToken);
            if (question != null) question.FollowerCount -= 1;

            appuser.AddFollowQuestion(questionId);
        }
    }
}
