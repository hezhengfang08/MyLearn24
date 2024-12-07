using MySelf.Zhihu.Core.Common;
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

        public IEnumerable<AnswerLike> AnswerLikes => answerLikes;

        public int AddLike(AnswerLike answerLike)
        {
            if (answerLike.IsLike)
            {
                LikeCount += 1;
            }
            else
            {
                DislikeCount += 1;
            }

            answerLikes.Add(answerLike);

            return LikeCount;
        }
        //细节逻辑不对
        public int RemoveLike(AnswerLike answerLike)
        {
            if (answerLike.IsLike)
            {
                LikeCount += 1;
            }
            else
            {
                DislikeCount += 1;
            }

            answerLikes.Remove(answerLike);

            return LikeCount;
        }
    }

}
