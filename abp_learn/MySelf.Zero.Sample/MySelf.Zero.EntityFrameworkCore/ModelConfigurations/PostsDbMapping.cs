using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore;
using MySelf.Zero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.EntityFrameworkCore.ModelConfigurations
{
    public class PostsDbMapping : IEntityTypeConfiguration<PostsEntity>
    {
        public void Configure(EntityTypeBuilder<PostsEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id).HasName("pk_posts_id");

            // Table & Column Mappings
            builder.ToTable("posts");

            builder.Property(m => m.Id)
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .IsRequired()
                .HasColumnName("id")
                .HasComment("主键标识");

            builder.Property(t => t.UserId).IsRequired()
                .HasColumnName("user_id").HasComment("用户id");
            builder.Property(t => t.PostContent).IsRequired()
                .HasColumnName("post_content").HasComment("回复内容");
            builder.Property(t => t.IpAddress).IsRequired()
                .HasColumnName("ip_address").HasComment("IP地址");
            builder.Property(t => t.RecivedPostId)
                .HasColumnName("recived_post_id").HasComment("回复的评论的id");
            builder.Property(t => t.IsRead).HasDefaultValue(false)
                .HasColumnName("is_read").HasColumnType("bit(1)").HasComment("是否已读");
            builder.Property(t => t.CreationTime).IsRequired()
                .HasColumnName("creation_time").HasComment("创建时间");

            builder.HasOne(t => t.Topic).WithMany(t => t.Postses).HasForeignKey("topic_id");
        }
    }

}
