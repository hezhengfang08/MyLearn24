using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zhaoxi.Forum.DbMigrator.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识"),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "名称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "varchar(2000)", maxLength: 2000, nullable: false, comment: "描叙")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_locked = table.Column<ulong>(type: "bit(1)", nullable: true, defaultValue: 0ul, comment: "是否锁定"),
                    sort_order = table.Column<int>(type: "int", nullable: false, comment: "板块排序"),
                    image = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false, comment: "图像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    pinyin = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "拼音")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    parent_category = table.Column<long>(type: "bigint", nullable: true, comment: "父板块"),
                    enabled = table.Column<ulong>(type: "bit(1)", nullable: true, defaultValue: 1ul, comment: "是否启用"),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_id", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "topic",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false, comment: "主键标识")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    user_id = table.Column<long>(type: "bigint", nullable: false, comment: "用户id"),
                    topic_name = table.Column<string>(type: "longtext", nullable: false, comment: "主题")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    topic_content = table.Column<string>(type: "longtext", nullable: false, comment: "内容")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    views = table.Column<int>(type: "int", nullable: false, comment: "流量数"),
                    favourite_times = table.Column<int>(type: "int", nullable: false, comment: "收藏数"),
                    is_locked = table.Column<ulong>(type: "bit(1)", nullable: true, defaultValue: 0ul, comment: "是否锁定"),
                    last_recive_userid = table.Column<int>(type: "int", nullable: true, comment: "最后回复人"),
                    last_recive_time = table.Column<DateTime>(type: "datetime(6)", nullable: true, comment: "最后回复时间"),
                    commits = table.Column<int>(type: "int", nullable: false, comment: "评论数"),
                    update_date = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "更新时间"),
                    hoted = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "是否热门"),
                    creamed = table.Column<bool>(type: "tinyint(1)", nullable: true, comment: "是否精华"),
                    category_id = table.Column<long>(type: "bigint", nullable: false),
                    creation_time = table.Column<DateTime>(type: "datetime(6)", nullable: false, comment: "创建时间")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_topic_id", x => x.id);
                    table.ForeignKey(
                        name: "FK_topic_category_category_id",
                        column: x => x.category_id,
                        principalTable: "category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_topic_category_id",
                table: "topic",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "topic");

            migrationBuilder.DropTable(
                name: "category");
        }
    }
}
