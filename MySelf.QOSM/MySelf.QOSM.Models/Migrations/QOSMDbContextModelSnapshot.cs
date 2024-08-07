﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MySelf.QOSM.Models.Context;

#nullable disable

namespace MySelf.QOSM.Models.Migrations
{
    [DbContext(typeof(QOSMDbContext))]
    partial class QOSMDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.CustomerInfo", b =>
                {
                    b.Property<int>("CustomerId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CustomerId"), 1L, 1);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("CustomerNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("CustomerPwd")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool?>("CustomerState")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValueSql("((1))");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsLogin")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("LogInTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LogOffTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("Sex")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(1)
                        .HasColumnType("nvarchar(1)")
                        .HasDefaultValueSql("(N'男')");

                    b.HasKey("CustomerId");

                    b.ToTable("CustomerInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.CustomerOrderInfo", b =>
                {
                    b.Property<int>("Rfid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RFId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Rfid"), 1L, 1);

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<int>("OrderCount")
                        .HasColumnType("int");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("OrderRemark")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Rfid");

                    b.HasIndex("FoodId");

                    b.HasIndex("OrderId");

                    b.ToTable("CustomerOrderInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.FoodInfo", b =>
                {
                    b.Property<int>("FoodId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FoodId"), 1L, 1);

                    b.Property<string>("FoodImg")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FoodName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("FoodPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int?>("FoodTypeInfoFtypeId")
                        .HasColumnType("int");

                    b.Property<int>("FtypeId")
                        .HasColumnType("int")
                        .HasColumnName("FTypeId");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOn")
                        .HasColumnType("bit");

                    b.Property<decimal>("PackAmount")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("decimal(18,2)")
                        .HasDefaultValueSql("((0.00))");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("FoodId");

                    b.HasIndex("FoodTypeInfoFtypeId");

                    b.ToTable("FoodInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.FoodOrderInfo", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"), 1L, 1);

                    b.Property<DateTime?>("CompleteTime")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DeliveryTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsPay")
                        .HasColumnType("bit");

                    b.Property<string>("OrderNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime");

                    b.Property<int>("PayWay")
                        .HasColumnType("int");

                    b.Property<string>("PickCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPackAmount")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("OrderId");

                    b.ToTable("FoodOrderInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.FoodTypeInfo", b =>
                {
                    b.Property<int>("FtypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("FTypeId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FtypeId"), 1L, 1);

                    b.Property<string>("FtypeName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("FTypeName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("FtypeId");

                    b.ToTable("FoodTypeInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.MenuInfo", b =>
                {
                    b.Property<int>("MenuId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("MenuId"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSysMenu")
                        .HasColumnType("bit");

                    b.Property<string>("MenuName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("MenuOrder")
                        .HasColumnType("int");

                    b.Property<string>("MenuPic")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MenuUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("MenuURL");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<int?>("RoleInfoRoleId")
                        .HasColumnType("int");

                    b.HasKey("MenuId");

                    b.HasIndex("RoleInfoRoleId");

                    b.ToTable("MenuInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.RoleInfo", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"), 1L, 1);

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("RoleName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("RoleId");

                    b.ToTable("RoleInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.RoleMenuInfo", b =>
                {
                    b.Property<int>("Rmid")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("RMId");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Rmid"), 1L, 1);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.HasKey("Rmid");

                    b.ToTable("RoleMenuInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.UserInfo", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"), 1L, 1);

                    b.Property<DateTime>("CreateTime")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<string>("UserPwd")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<bool>("UserState")
                        .HasColumnType("bit");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.ViewCustomerOrderInfo", b =>
                {
                    b.Property<DateTime?>("CompleteTime")
                        .HasColumnType("datetime");

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime?>("DeliveryTime")
                        .HasColumnType("datetime");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<string>("OrderNo")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("OrderState")
                        .HasColumnType("int");

                    b.Property<DateTime>("OrderTime")
                        .HasColumnType("datetime");

                    b.Property<int>("PayWay")
                        .HasColumnType("int");

                    b.Property<string>("PickCode")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<decimal>("TotalAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<decimal>("TotalPackAmount")
                        .HasColumnType("decimal(18,2)");

                    b.ToView("ViewCustomerOrderInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.ViewFoodInfo", b =>
                {
                    b.Property<int>("FoodId")
                        .HasColumnType("int");

                    b.Property<string>("FoodImg")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("FoodName")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("FoodPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("FtypeId")
                        .HasColumnType("int")
                        .HasColumnName("FTypeId");

                    b.Property<string>("FtypeName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)")
                        .HasColumnName("FTypeName");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsOn")
                        .HasColumnType("bit");

                    b.Property<decimal>("PackAmount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Remark")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.ToView("ViewFoodInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.ViewMenuInfo", b =>
                {
                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsSysMenu")
                        .HasColumnType("bit");

                    b.Property<int>("MenuId")
                        .HasColumnType("int");

                    b.Property<string>("MenuName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<int>("MenuOrder")
                        .HasColumnType("int");

                    b.Property<string>("MenuPic")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)");

                    b.Property<string>("MenuUrl")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)")
                        .HasColumnName("MenuURL");

                    b.Property<int>("ParentId")
                        .HasColumnType("int");

                    b.Property<string>("ParentName")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.ToView("ViewMenuInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.ViewUserInfo", b =>
                {
                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("RoleName")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)");

                    b.Property<bool>("UserState")
                        .HasColumnType("bit");

                    b.ToView("ViewUserInfos");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.CustomerOrderInfo", b =>
                {
                    b.HasOne("MySelf.QOSM.Models.Entities.FoodInfo", "Food")
                        .WithMany()
                        .HasForeignKey("FoodId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MySelf.QOSM.Models.Entities.FoodOrderInfo", "Order")
                        .WithMany()
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Food");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.FoodInfo", b =>
                {
                    b.HasOne("MySelf.QOSM.Models.Entities.FoodTypeInfo", null)
                        .WithMany("Foods")
                        .HasForeignKey("FoodTypeInfoFtypeId");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.MenuInfo", b =>
                {
                    b.HasOne("MySelf.QOSM.Models.Entities.RoleInfo", null)
                        .WithMany("Menus")
                        .HasForeignKey("RoleInfoRoleId");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.UserInfo", b =>
                {
                    b.HasOne("MySelf.QOSM.Models.Entities.RoleInfo", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.FoodTypeInfo", b =>
                {
                    b.Navigation("Foods");
                });

            modelBuilder.Entity("MySelf.QOSM.Models.Entities.RoleInfo", b =>
                {
                    b.Navigation("Menus");

                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
