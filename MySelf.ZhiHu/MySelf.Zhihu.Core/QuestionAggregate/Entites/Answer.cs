using MySelf.Zhihu.Core.Common;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Entites
{
    public class Answer : AuditWithUserEntity
    {
        private readonly List<AnswerLike> answerLikes = new();
  
        public string Content { get; set; } = null!;
        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;

        public int DislikeCount { get; private set; }

        public int LikeCount { get; private set; }

        /// <summary>
        /// 点赞/点踩记录列表
        /// </summary>
        public IReadOnlyCollection<AnswerLike> AnswerLikes => answerLikes.AsReadOnly();

        /// <summary>
        ///     添加点赞/点踩记录
        /// </summary>
        /// <param name="userId">点赞用户</param>
        /// <param name="isLike">赞or踩</param>
        /// <returns></returns>
        public Result AddLike(int userId, bool isLike)
        {
            var answerLike = answerLikes.FirstOrDefault(like => like.UserId == userId);

            if (answerLike != null) return Result.Failure("已赞或已踩");

            answerLike = new AnswerLike
            {
                UserId = userId,
                IsLike = isLike
            };
            answerLikes.Add(answerLike);

            if (isLike) LikeCount += 1;
            else DislikeCount += 1;

            return Result.Success();
        }

        /// <summary>
        ///     更新点赞/点踩记录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="isLike"></param>
        /// <returns></returns>
        public Result UpdateLike(int userId, bool isLike)
        {
            var answerLike = answerLikes.FirstOrDefault(like => like.UserId == userId);

            if (answerLike == null) return Result.NotFound("未找到点赞记录");

            if (answerLike.IsLike == isLike) return Result.Failure("已赞或已踩");

            answerLike.IsLike = isLike;

            if (isLike)
            {
                LikeCount += 1;
                DislikeCount -= 1;
            }
            else
            {
                DislikeCount += 1;
                LikeCount -= 1;
            }

            return Result.Success();
        }

        /// <summary>
        ///     移除点赞/点踩记录
        /// </summary>
        /// <param name="userId"></param>
        public void RemoveLike(int userId)
        {
            var answerLike = answerLikes.FirstOrDefault(like => like.UserId == userId);

            if (answerLike == null) return;

            answerLikes.Remove(answerLike);

            if (answerLike.IsLike) LikeCount -= 1;
            else DislikeCount -= 1;
        }
    }


}
