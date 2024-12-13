using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Specifications
{
    public class FollowQuestionByIdSpec:Specification<AppUser>
    {
        public FollowQuestionByIdSpec(int userId, int questionId)
        {
            FilterCondition = user => user.Id.Equals(userId);

            AddInclude(user => user.FollowQuestions.Where(fq => fq.QuestionId.Equals(questionId)));
        }
    }
}
