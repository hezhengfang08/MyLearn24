using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Common.Interfaces
{
    public interface IFollowUserQueryService
    {
        Task<int[]> GetFollowerIdsAsync(int userId);
    }

}
