using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Configuration
{
    public class OutboxFeedConfiguration : IEntityTypeConfiguration<Core.Entites.Outbox>
    {
        public void Configure(EntityTypeBuilder<Core.Entites.Outbox> builder)
        {
            builder.Property(p => p.FeedType)
                .HasColumnType("tinyint")
                .IsRequired();
        }
    }
}
