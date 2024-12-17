using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.AppUserAggregate.Specifications;
using MySelf.Zhihu.Core.Interfaces;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Services
{
    public class AppUserService(
        IReadRepository<AppUser> appUsers,
        IReadRepository<Question> questions) : IAppUserService
    {
        /// <summary>
        ///     关注问题
        /// </summary>
        /// <param name="appuser">用户</param>
        /// <param name="questionId">关注问题ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> FollowQuestionAsync(AppUser appuser, int questionId, CancellationToken cancellationToken)
        {
            if (await questions.CountAsync(new QuestionByIdSpec(questionId), cancellationToken) == 0)
                return Result.NotFound("关注问题不存在");

            return appuser.AddFollowQuestion(questionId);
        }

        /// <summary>
        ///     关注用户
        /// </summary>
        /// <param name="appuser">用户</param>
        /// <param name="followeeId">关注用户ID</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result> FolloweeUserAsync(AppUser appuser, int followeeId, CancellationToken cancellationToken)
        {
            if (await appUsers.CountAsync(new AppUserByIdSpec(followeeId), cancellationToken) == 0)
                return Result.NotFound("关注用户不存在");

            return appuser.AddFollowee(followeeId);
        }
    }
}
