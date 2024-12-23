using MassTransit;
using MySelf.Zhihu.Feed.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Contexts
{
    public class FeedDbContext(DbContextOptions<FeedDbContext> options) : DbContext(options)
    {
        public DbSet<Outbox> Outbox => Set<Outbox>();

        public DbSet<Inbox> Inbox => Set<Inbox>();

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }


}
