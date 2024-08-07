using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.Domain.Entities;
using Zhaoxi.Forum.EntityFrameworkCore.ModelConfigurations;

namespace Zhaoxi.Forum.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class ForumDbContext : AbpDbContext<ForumDbContext>
{
    public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfiguration(new CategoryDbMapping())
            .ApplyConfiguration(new TopicDbMapping());
    }

    public DbSet<CategoryEntity> Categories { get; set; }

    public DbSet<TopicEntity> Topic { get; set; }

}