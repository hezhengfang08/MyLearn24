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
        public ICollection<FollowUser> Followees { get; set; } = new List<FollowUser>();

        /// <summary>
        ///   粉丝列表
        /// </summary>
        public ICollection<FollowUser> Followers { get; set; } =  new List<FollowUser>();
       


        /// <summary>
        ///     关注问题列表
        /// </summary>
        private readonly List<FollowQuestion> followQuestions = [];
        public IEnumerable<FollowQuestion> FollowQuestions => followQuestions;

        /// <summary>
        ///    添加关注问题
        /// </summary>
        /// <param name="questionId"></param>
        /// <returns></returns>
        public IResult AddFollowQuestion(int questionId)
        {
            if (FollowQuestions.Any(fq => fq.QuestionId == questionId)) return Result.Invalid("问题已关注");

            var followQuestion = new FollowQuestion
            {
                QuestionId = questionId,
                FollowDate = DateTimeOffset.Now
            };

            followQuestions.Add(followQuestion);
            AddDomainEvent(new FollowQuestionAddedEvent(followQuestion));
            return Result.Success();
        }

        /// <summary>
        ///     移除关注问题
        /// </summary>
        /// <param name="questionId"></param>
        public void RemoveFollowQuestion(int questionId)
        {
            var followQuestion = FollowQuestions.FirstOrDefault(fq => fq.QuestionId == questionId);
            if (followQuestion == null) return;
            followQuestions.Remove(followQuestion);
            AddDomainEvent(new FollowQuestionRemovedEvent(followQuestion));
        }
    }
}
