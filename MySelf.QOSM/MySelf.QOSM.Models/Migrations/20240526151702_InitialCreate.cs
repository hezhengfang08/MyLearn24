using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySelf.QOSM.Models.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CustomerInfos",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CustomerPwd = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Sex = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, defaultValueSql: "(N'男')"),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    CustomerState = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    LogOffTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfos", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerOrderInfos",
                columns: table => new
                {
                    RFId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    FoodId = table.Column<int>(type: "int", nullable: false),
                    OrderCount = table.Column<int>(type: "int", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderRemark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerOrderInfos", x => x.RFId);
                });

            migrationBuilder.CreateTable(
                name: "FoodInfos",
                columns: table => new
                {
                    FoodId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FoodName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FTypeId = table.Column<int>(type: "int", nullable: false),
                    FoodPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FoodImg = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    PackAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false, defaultValueSql: "((0.00))"),
                    IsOn = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodInfos", x => x.FoodId);
                });

            migrationBuilder.CreateTable(
                name: "FoodOrderInfos",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrderNo = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TotalPackAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PayWay = table.Column<int>(type: "int", nullable: false),
                    IsPay = table.Column<bool>(type: "bit", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: false),
                    OrderTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeliveryTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CompleteTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    PickCode = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodOrderInfos", x => x.OrderId);
                });

            migrationBuilder.CreateTable(
                name: "FoodTypeInfos",
                columns: table => new
                {
                    FTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FTypeName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodTypeInfos", x => x.FTypeId);
                });

            migrationBuilder.CreateTable(
                name: "MenuInfos",
                columns: table => new
                {
                    MenuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MenuName = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    ParentId = table.Column<int>(type: "int", nullable: false),
                    MenuPic = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    MenuURL = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MenuOrder = table.Column<int>(type: "int", nullable: false),
                    IsSysMenu = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuInfos", x => x.MenuId);
                });

            migrationBuilder.CreateTable(
                name: "RoleInfos",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsAdmin = table.Column<bool>(type: "bit", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleInfos", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "RoleMenuInfos",
                columns: table => new
                {
                    RMId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    MenuId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleMenuInfos", x => x.RMId);
                });

            migrationBuilder.CreateTable(
                name: "UserInfos",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    UserPwd = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UserState = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserInfos", x => x.UserId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerInfos");

            migrationBuilder.DropTable(
                name: "CustomerOrderInfos");

            migrationBuilder.DropTable(
                name: "FoodInfos");

            migrationBuilder.DropTable(
                name: "FoodOrderInfos");

            migrationBuilder.DropTable(
                name: "FoodTypeInfos");

            migrationBuilder.DropTable(
                name: "MenuInfos");

            migrationBuilder.DropTable(
                name: "RoleInfos");

            migrationBuilder.DropTable(
                name: "RoleMenuInfos");

            migrationBuilder.DropTable(
                name: "UserInfos");
        }
    }
}
