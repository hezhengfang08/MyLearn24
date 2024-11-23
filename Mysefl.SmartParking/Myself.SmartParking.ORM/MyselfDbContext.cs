using Microsoft.EntityFrameworkCore;
using Myself.SmartParking.Entities;
using System.Collections.Generic;

namespace Myself.SmartParking.ORM
{
      public class ZhaoxiDbContext : DbContext
    {
        public ZhaoxiDbContext(DbContextOptions<ZhaoxiDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlite("Data Source=data.db");
        }

        public virtual DbSet<SysUser> SysUser { get; set; }
    }
}
