using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Entites
{
    public class FollowUser
    {
        /// <summary>
        /// 关注者
        /// </summary>
        public int FollowerId { get; set; }
        public AppUser Follower { get; set; } = null!;

        // 粉丝
        public int FolloweeId { get; set; }
        public AppUser Followee { get; set; } = null!;

        public DateTimeOffset FollowDate { get; set; }
    }
}
