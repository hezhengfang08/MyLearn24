using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MySelf.QOSM.Models.Migrations
{
    public partial class InitialCreate0724 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleInfoRoleId",
                table: "MenuInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FoodTypeInfoFtypeId",
                table: "FoodInfos",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsLogin",
                table: "CustomerInfos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "LogInTime",
                table: "CustomerInfos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuInfos_RoleInfoRoleId",
                table: "MenuInfos",
                column: "RoleInfoRoleId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodInfos_FoodTypeInfoFtypeId",
                table: "FoodInfos",
                column: "FoodTypeInfoFtypeId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderInfos_FoodId",
                table: "CustomerOrderInfos",
                column: "FoodId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerOrderInfos_OrderId",
                table: "CustomerOrderInfos",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderInfos_FoodInfos_FoodId",
                table: "CustomerOrderInfos",
                column: "FoodId",
                principalTable: "FoodInfos",
                principalColumn: "FoodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerOrderInfos_FoodOrderInfos_OrderId",
                table: "CustomerOrderInfos",
                column: "OrderId",
                principalTable: "FoodOrderInfos",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FoodInfos_FoodTypeInfos_FoodTypeInfoFtypeId",
                table: "FoodInfos",
                column: "FoodTypeInfoFtypeId",
                principalTable: "FoodTypeInfos",
                principalColumn: "FTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_MenuInfos_RoleInfos_RoleInfoRoleId",
                table: "MenuInfos",
                column: "RoleInfoRoleId",
                principalTable: "RoleInfos",
                principalColumn: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserInfos_RoleInfos_RoleId",
                table: "UserInfos",
                column: "RoleId",
                principalTable: "RoleInfos",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderInfos_FoodInfos_FoodId",
                table: "CustomerOrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_CustomerOrderInfos_FoodOrderInfos_OrderId",
                table: "CustomerOrderInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_FoodInfos_FoodTypeInfos_FoodTypeInfoFtypeId",
                table: "FoodInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuInfos_RoleInfos_RoleInfoRoleId",
                table: "MenuInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_UserInfos_RoleInfos_RoleId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_UserInfos_RoleId",
                table: "UserInfos");

            migrationBuilder.DropIndex(
                name: "IX_MenuInfos_RoleInfoRoleId",
                table: "MenuInfos");

            migrationBuilder.DropIndex(
                name: "IX_FoodInfos_FoodTypeInfoFtypeId",
                table: "FoodInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderInfos_FoodId",
                table: "CustomerOrderInfos");

            migrationBuilder.DropIndex(
                name: "IX_CustomerOrderInfos_OrderId",
                table: "CustomerOrderInfos");

            migrationBuilder.DropColumn(
                name: "RoleInfoRoleId",
                table: "MenuInfos");

            migrationBuilder.DropColumn(
                name: "FoodTypeInfoFtypeId",
                table: "FoodInfos");

            migrationBuilder.DropColumn(
                name: "IsLogin",
                table: "CustomerInfos");

            migrationBuilder.DropColumn(
                name: "LogInTime",
                table: "CustomerInfos");
        }
    }
}
