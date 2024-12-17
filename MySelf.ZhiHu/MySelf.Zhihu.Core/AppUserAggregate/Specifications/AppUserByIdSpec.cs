using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Specifications
{
    public class AppUserByIdSpec : Specification<AppUser>
    {
        public AppUserByIdSpec(int userId)
        {
            FilterCondition = user => user.Id == userId;
        }
    }
}
