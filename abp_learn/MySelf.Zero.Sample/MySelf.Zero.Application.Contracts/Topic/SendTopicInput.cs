using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Topic
{
    public class SendTopicInput
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 主题名
        /// </summary>
        public string TopicName { get; set; }

        /// <summary>
        /// 主题内容
        /// </summary>
        public string TopicContent { get; set; }

        /// <summary>
        /// 板块Id
        /// </summary>
        public long CategoryId { get; set; }
    }
}
