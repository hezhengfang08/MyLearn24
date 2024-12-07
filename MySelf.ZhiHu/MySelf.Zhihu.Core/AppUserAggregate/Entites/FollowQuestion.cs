using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Entites
{
    public class FollowQuestion
    {
        public int UserId { get; set; } 
        public AppUser AppUser { get; set; }
        public int QuestionId { get; set; }

        public DateTimeOffset FollowDate { get; set; }

    }
}
