using Microsoft.EntityFrameworkCore;
using MySelf.QOSM.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Context
{
    public partial class QOSMDbContext : DbContext
    {
        public QOSMDbContext()
        {
        }

        public QOSMDbContext(DbContextOptions<QOSMDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CustomerInfo> CustomerInfos { get; set; }
        public virtual DbSet<CustomerOrderInfo> CustomerOrderInfos { get; set; }
        public virtual DbSet<FoodInfo> FoodInfos { get; set; }
        public virtual DbSet<FoodOrderInfo> FoodOrderInfos { get; set; }
        public virtual DbSet<FoodTypeInfo> FoodTypeInfos { get; set; }
        public virtual DbSet<MenuInfo> MenuInfos { get; set; }
        public virtual DbSet<RoleInfo> RoleInfos { get; set; }
        public virtual DbSet<RoleMenuInfo> RoleMenuInfos { get; set; }
        public virtual DbSet<UserInfo> UserInfos { get; set; }
        public virtual DbSet<ViewCustomerOrderInfo> ViewCustomerOrderInfos { get; set; }
        public virtual DbSet<ViewFoodInfo> ViewFoodInfos { get; set; }
        public virtual DbSet<ViewMenuInfo> ViewMenuInfos { get; set; }
        public virtual DbSet<ViewUserInfo> ViewUserInfos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connStr = "Data Source=.;Initial Catalog=QuickOrderSystem;User ID=sa;Password=hzf0804.";
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerInfo>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.Address).HasMaxLength(100);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CustomerName).HasMaxLength(10);

                entity.Property(e => e.CustomerNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CustomerState)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.LogOffTime).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Sex)
                    .HasMaxLength(1)
                    .HasDefaultValueSql("(N'男')");
            });

            modelBuilder.Entity<CustomerOrderInfo>(entity =>
            {
                entity.HasKey(e => e.Rfid);

                entity.Property(e => e.Rfid).HasColumnName("RFId");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.OrderRemark).HasMaxLength(500);
            });

            modelBuilder.Entity<FoodInfo>(entity =>
            {
                entity.HasKey(e => e.FoodId);

                entity.Property(e => e.FoodImg)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FoodName).HasMaxLength(50);

                entity.Property(e => e.FoodPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FtypeId).HasColumnName("FTypeId");

                entity.Property(e => e.PackAmount)
                    .HasColumnType("decimal(18, 2)")
                    .HasDefaultValueSql("((0.00))");

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<FoodOrderInfo>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.Property(e => e.CompleteTime).HasColumnType("datetime");

                entity.Property(e => e.DeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.OrderNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.PickCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPackAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<FoodTypeInfo>(entity =>
            {
                entity.HasKey(e => e.FtypeId);

                entity.Property(e => e.FtypeId).HasColumnName("FTypeId");

                entity.Property(e => e.FtypeName)
                    .HasMaxLength(10)
                    .HasColumnName("FTypeName");

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<MenuInfo>(entity =>
            {
                entity.HasKey(e => e.MenuId);

                entity.Property(e => e.MenuName).HasMaxLength(20);

                entity.Property(e => e.MenuPic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MenuUrl)
                    .HasMaxLength(100)
                    .HasColumnName("MenuURL");
            });

            modelBuilder.Entity<RoleInfo>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.Remark).HasMaxLength(500);

                entity.Property(e => e.RoleName).HasMaxLength(10);
            });

            modelBuilder.Entity<RoleMenuInfo>(entity =>
            {
                entity.HasKey(e => e.Rmid);

                entity.Property(e => e.Rmid).HasColumnName("RMId");
            });

            modelBuilder.Entity<UserInfo>(entity =>
            {
                entity.HasKey(e => e.UserId);

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.UserPwd)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<ViewCustomerOrderInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewCustomerOrderInfos");

                entity.Property(e => e.CompleteTime).HasColumnType("datetime");

                entity.Property(e => e.CustomerName).HasMaxLength(10);

                entity.Property(e => e.DeliveryTime).HasColumnType("datetime");

                entity.Property(e => e.OrderNo)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.PickCode)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TotalPackAmount).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<ViewFoodInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewFoodInfos");

                entity.Property(e => e.FoodImg)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FoodName).HasMaxLength(50);

                entity.Property(e => e.FoodPrice).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FtypeId).HasColumnName("FTypeId");

                entity.Property(e => e.FtypeName)
                    .HasMaxLength(10)
                    .HasColumnName("FTypeName");

                entity.Property(e => e.PackAmount).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Remark).HasMaxLength(500);
            });

            modelBuilder.Entity<ViewMenuInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewMenuInfos");

                entity.Property(e => e.MenuName).HasMaxLength(20);

                entity.Property(e => e.MenuPic)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.MenuUrl)
                    .HasMaxLength(100)
                    .HasColumnName("MenuURL");

                entity.Property(e => e.ParentName).HasMaxLength(20);
            });

            modelBuilder.Entity<ViewUserInfo>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("ViewUserInfos");

                entity.Property(e => e.CreateTime).HasColumnType("datetime");

                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleName).HasMaxLength(10);

                entity.Property(e => e.UserName)
                    .HasMaxLength(20)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}

