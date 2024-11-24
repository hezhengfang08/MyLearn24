
using Microsoft.EntityFrameworkCore;
using Myself.SmartParking.Entities;
using System.Collections.Generic;

namespace Myself.SmartParking.ORM
{
      public class MyselfDbContext : DbContext
    {
        public MyselfDbContext(DbContextOptions<MyselfDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=SP2024;Integrated Security=True;Trust Server Certificate=True");
        }

        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
    }
}
