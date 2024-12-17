using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.Interfaces
{
    public interface IAppUserService
    {
        Task<Result> FollowQuestionAsync(AppUser appuser, int questionId, CancellationToken cancellationToken);

        Task<Result> FolloweeUserAsync(AppUser appuser, int foloweeId, CancellationToken cancellationToken);
    }
}
