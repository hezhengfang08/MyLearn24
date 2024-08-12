using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Forum.Application.Contracts.Topic
{
    public class ReplyPostsInput
    {
        /// <summary>
        /// 主题id
        /// </summary>
        public long TopicId { get; set; }

        /// <summary>
        /// 回复人id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        public string Content { get; set; }

        ///// <summary>
        ///// 某一个回复
        ///// </summary>
        //public long? PostId { get; set; }
    }
}
