using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Specifications
{
    public class AnswerByIdWithLikeByUserIdSpec : Specification<Answer>
    {
        public AnswerByIdWithLikeByUserIdSpec(int answerId, int userId)
        {
            FilterCondition = answer => answer.Id == answerId;
            AddInclude(answer => answer.AnswerLikes.Where(al => al.UserId == userId));
        }
    }

}
