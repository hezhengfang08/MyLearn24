using Magicodes.ExporterAndImporter.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Topic
{
    public class TopicImportDto
    {
        /// <summary>
        /// 用户
        /// </summary>
        [ImporterHeader(Name = "用户")]
        public long UserId { get; set; }

        /// <summary>
        /// 板块
        /// </summary>
        [ImporterHeader(Name = "板块")]
        public long CategoryId { get; set; }

        /// <summary>
        /// 标题
        /// </summary>
        [ImporterHeader(Name = "标题")]
        public string Title { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        [ImporterHeader(Name = "内容")]
        public string Content { get; set; }
    }

}
