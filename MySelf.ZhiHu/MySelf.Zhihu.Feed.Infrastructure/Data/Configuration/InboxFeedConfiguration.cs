using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zhihu.Feed.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Configuration
{
    public class InboxFeedConfiguration : IEntityTypeConfiguration<Inbox>
    {
        public void Configure(EntityTypeBuilder<Inbox> builder)
        {
            builder.Property(p => p.FeedType)
                .HasColumnType("tinyint")
                .IsRequired();
        }
    }

}
