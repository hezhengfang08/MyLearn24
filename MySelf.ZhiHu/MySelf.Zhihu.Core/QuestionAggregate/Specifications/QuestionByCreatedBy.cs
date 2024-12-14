using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Specifications
{
    public class QuestionByCreatedBy : Specification<Question>
    {
        public QuestionByCreatedBy(int userId, int questionId) {
        
            FilterCondition = q=>q.Id == questionId && q.CreatedBy == userId;   
        }
    }
}
