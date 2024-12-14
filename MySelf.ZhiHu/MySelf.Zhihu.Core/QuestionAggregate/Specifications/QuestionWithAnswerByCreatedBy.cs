using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Specifications
{
    public class QuestionWithAnswerByCreatedBy:Specification<Question>
    {
        public QuestionWithAnswerByCreatedBy(int userId, int questionId)
        {
           FilterCondition = q=>q.Id == questionId && q.CreatedBy == userId;
            AddInclude(q=>q.Answers.Take(1));
        }
    }
}
