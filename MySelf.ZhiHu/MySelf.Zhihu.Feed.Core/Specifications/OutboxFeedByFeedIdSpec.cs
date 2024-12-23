using MySelf.Zhihu.Feed.Core.Entites;
using MySelf.Zhihu.SharedKernel.Specification;
using MySelf.Zhihu.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Core.Specifications
{
    public class OutboxFeedByFeedIdSpec : Specification<Outbox>
    {
        public OutboxFeedByFeedIdSpec(int feedId, FeedType feedType)
        {
            FilterCondition = f => f.FeedId == feedId && f.FeedType == feedType;
        }
    }

}
