using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases
{
    public record FeedDto
    {
        public int FeedId { get; set; }

        public FeedType FeedType { get; set; }

        public int SenderId { get; set; }
    };
}
