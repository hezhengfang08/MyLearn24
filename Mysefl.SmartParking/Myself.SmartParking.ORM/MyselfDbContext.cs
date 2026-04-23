
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
            // 联合主键
            modelBuilder.Entity<RoleMenu>()
                .HasKey(pk => new { pk.RoleId, pk.MenuId });
            modelBuilder.Entity<RoleUser>()
                .HasKey(pk => new { pk.RoleId, pk.UserId });
            // 角色表的一对多关系
            modelBuilder.Entity<RoleUser>()
                .HasOne(u => u.SysRole)
                .WithMany(r => r.Users)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<RoleMenu>()
                .HasOne(u => u.SysRole)
                .WithMany(r => r.Menus)
                .OnDelete(DeleteBehavior.Cascade);
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
        public virtual DbSet<SysRole> SysRole { get; set; }

        public virtual DbSet<RoleMenu> RoleMenu { get; set; }
        public virtual DbSet<RoleUser> RoleUser { get; set; }
    }
}
