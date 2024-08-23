using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Domain.Entities
{
    public class PostsEntity : BaseEntity
    {
        public long UserId { get; set; }

        public string PostContent { get; set; }

        public string IpAddress { get; set; }

        /// <summary>
        /// 回复的评论的id
        /// </summary>
        public long? RecivedPostId { get; set; }

        public bool? IsRead { get; set; } = false;

        public virtual TopicEntity Topic { get; set; }
    }

}
