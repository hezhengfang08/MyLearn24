using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Specifications;
using MySelf.Zhihu.SharedKernel.Repositoy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Data
{
    public interface IAnswerRepositoy : IGenericRepository<Answer>
    {
        Task<Answer?> GetAnswerByIdWithLikeByUserIdAsync(AnswerByIdWithLikeByUserIdSpec specification,
            CancellationToken cancellationToken = default);
    }
}
