using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Specifications
{
    public class FolloweeUserByIdSpec : Specification<AppUser>
    {
        public FolloweeUserByIdSpec(int userId, int followeeId)
        {
            FilterCondition = user => user.Id == userId;
            AddInclude(user => user.Followees.Where(followUser => followUser.FolloweeId == followeeId));
        }
    }
}
