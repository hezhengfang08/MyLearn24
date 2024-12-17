using MySelf.Zhihu.Core.AppUserAggregate.Events;
using MySelf.Zhihu.Core.Common;
using MySelf.Zhihu.SharedKernel.Domain;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Entites
{
    public class AppUser:BaseAuditEntity,IAggregateRoot
    {

        private readonly List<FollowUser> _followees = [];
        private readonly List<FollowUser> _followers = [];
        private readonly List<FollowQuestion> _followQuestions = [];
        protected AppUser() { }
        public AppUser(int userId) {
            Id  = userId; 
        }
        public string? Nickname { get; set; }

        public string? Avatar { get; set; }

        public string? Bio { get; set; }
        /// <summary>
        ///     关注列表
        /// </summary>
        public IReadOnlyCollection<FollowUser> Followees => _followees.AsReadOnly();

        /// <summary>
        ///     粉丝列表
        /// </summary>
        public IReadOnlyCollection<FollowUser> Followers => _followers.AsReadOnly();

        /// <summary>
        ///     关注问题列表
        /// </summary>
        public IReadOnlyCollection<FollowQuestion> FollowQuestions => _followQuestions.AsReadOnly();

        /// <summary>
        ///     添加关注问题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public Result AddFollowQuestion(int questionId)
        {
            if (_followQuestions.Any(fq => fq.QuestionId == questionId)) return Result.Invalid("问题已关注");

            var followQuestion = new FollowQuestion
            {
                UserId = Id,
                QuestionId = questionId,
                FollowDate = DateTimeOffset.Now
            };

            _followQuestions.Add(followQuestion);

            AddDomainEvent(new FollowQuestionAddedEvent(followQuestion));

            return Result.Success();
        }

        /// <summary>
        ///     移除关注问题
        /// </summary>
        /// <param name="questionId"></param>
        public void RemoveFollowQuestion(int questionId)
        {
            var followQuestion = _followQuestions.FirstOrDefault(fq => fq.QuestionId == questionId);
            if (followQuestion == null) return;

            _followQuestions.Remove(followQuestion);

            AddDomainEvent(new FollowQuestionRemovedEvent(followQuestion));
        }

        /// <summary>
        ///     添加关注
        /// </summary>
        /// <param name="followeeId"></param>
        /// <returns></returns>
        public Result AddFollowee(int followeeId)
        {
            if (_followees.Any(fu => fu.FolloweeId == followeeId)) return Result.Invalid("该用户已关注");

            var followUser = new FollowUser
            {
                FollowerId = Id,
                FolloweeId = followeeId,
                FollowDate = DateTimeOffset.Now
            };

            _followees.Add(followUser);

            return Result.Success();
        }

        /// <summary>
        ///     移除关注
        /// </summary>
        /// <param name="followeeId"></param>
        public void RemoveFollowee(int followeeId)
        {
            var followUser = _followees.FirstOrDefault(fu => fu.FolloweeId == followeeId);
            if (followUser == null) return;

            _followees.Remove(followUser);
        }
    }
}
