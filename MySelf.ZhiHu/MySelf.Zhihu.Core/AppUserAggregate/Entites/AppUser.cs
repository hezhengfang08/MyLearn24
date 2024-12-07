using MySelf.Zhihu.Core.Common;
using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Core.AppUserAggregate.Entites
{
    public class AppUser:BaseAuditEntity,IAggregateRoot
    {
        protected AppUser() { }
        public AppUser(int userId) {
            Id  = userId; 
        }
        public string? Nickname { get; set; }

        public string? Avatar { get; set; }

        public string? Bio { get; set; }

        /// <summary>
        ///    
        /// </summary>
        public ICollection<FollowUser> Followees { get; set; } = new List<FollowUser>();

        public ICollection<FollowUser> Followers { get; set; } =  new List<FollowUser>();

        public ICollection<FollowQuestion> FollowQuestions { get; set; } = new List<FollowQuestion>();
    }
}
