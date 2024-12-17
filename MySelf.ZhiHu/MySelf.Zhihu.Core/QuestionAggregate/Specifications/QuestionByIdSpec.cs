using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.QuestionAggregate.Specifications
{
    public class QuestionByIdSpec : Specification<Question>
    {
        public QuestionByIdSpec(int id)
        {
            FilterCondition = question => question.Id == id;
        }
    }
}
