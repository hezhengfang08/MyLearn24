using MySelf.Zhihu.SharedKernel.Domain;
using MySelf.Zhihu.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Core.Entites
{
    public class Outbox : BaseEntity<int>, IAggregateRoot
    {
        public int UserId { get; set; }

        public int FeedId { get; set; }

        public FeedType FeedType { get; set; }

        public DateTimeOffset SentAt { get; set; } = DateTimeOffset.Now;
    }

}
