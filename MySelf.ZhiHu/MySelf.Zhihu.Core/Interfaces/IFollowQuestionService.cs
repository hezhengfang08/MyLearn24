using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Interfaces
{
    public interface IFollowQuestionService
    {
        Task<IResult> FollowAsync(AppUser appuser, int questionId, CancellationToken cancellationToken);
    }
}
