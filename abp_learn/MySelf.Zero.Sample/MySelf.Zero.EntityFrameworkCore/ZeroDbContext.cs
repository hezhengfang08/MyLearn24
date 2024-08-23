using Microsoft.EntityFrameworkCore;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.EntityFrameworkCore.ModelConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MySelf.Zero.EntityFrameworkCore
{
   
    public class ZeroDbContext : AbpDbContext<ZeroDbContext>
    {
        public ZeroDbContext(DbContextOptions<ZeroDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ZeroDbContext).Assembly);
            // modelBuilder.ApplyConfiguration(new CategoryDbMapping())
            //.ApplyConfiguration(new TopicDbMapping());
        }
        public DbSet<CategoryEntity> Categories { get; set; }

        public DbSet<TopicEntity> Topic { get; set; }

        public DbSet<PostsEntity> Posts { get; set; }

        public DbSet<UserEntity> User { get; set; }
    }
}
