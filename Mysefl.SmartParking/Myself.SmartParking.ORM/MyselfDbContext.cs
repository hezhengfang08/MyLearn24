
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Myself.SmartParking.Entities;
using System.Collections.Generic;
using System.Globalization;

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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 页面-》数据库的转换
            // 数据库-》页面的转换
            ValueConverter iconValueConverter =
                new ValueConverter<string, string>(
                    p2d => string.IsNullOrEmpty(p2d) ? null : ((int)p2d.ToArray()[0]).ToString("x"),
                    d2p => d2p == null ? "" : ((char)int.Parse(d2p, NumberStyles.HexNumber)).ToString()
                    );
            modelBuilder.Entity<SysMenu>()
                .Property(m => m.MenuIcon)
                .HasConversion(iconValueConverter);
        }
        public virtual DbSet<SysUser> SysUser { get; set; }
        public virtual DbSet<SysMenu> SysMenu { get; set; }
    }
}
