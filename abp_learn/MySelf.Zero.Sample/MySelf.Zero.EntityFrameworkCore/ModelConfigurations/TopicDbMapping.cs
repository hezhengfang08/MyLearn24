using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zero.Domain.Entities;


namespace MySelf.Zero.EntityFrameworkCore.ModelConfigurations
{
    public class TopicDbMapping : IEntityTypeConfiguration<TopicEntity>
    {
        public void Configure(EntityTypeBuilder<TopicEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id).HasName("pk_topic_id");

            // Table & Column Mappings
            builder.ToTable("topic");

            builder.Property(m => m.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
            .IsRequired()
            .HasColumnName("id")
            .HasComment("主键标识");

            builder.Property(t => t.UserId).IsRequired()
                .HasColumnName("user_id").HasComment("用户id");
            builder.Property(t => t.TopicName).IsRequired()
                .HasColumnName("topic_name").HasComment("主题");
            builder.Property(t => t.TopicContent).IsRequired()
                .HasColumnName("topic_content").HasComment("内容");
            builder.Property(t => t.Views)
                .HasColumnName("views").HasComment("流量数");
            builder.Property(t => t.FavouriteTimes)
                .HasColumnName("favourite_times").HasComment("收藏数");
            builder.Property(t => t.IsLocked).HasDefaultValue(false)
                .HasColumnName("is_locked").HasColumnType("bit(1)").HasComment("是否锁定");
            builder.Property(t => t.LastReciveUserId)
                .HasColumnName("last_recive_userid").HasComment("最后回复人");
            builder.Property(t => t.LastReciveTime)
                .HasColumnName("last_recive_time").HasComment("最后回复时间");
            builder.Property(t => t.Commits)
                .HasColumnName("commits").HasComment("评论数");
            builder.Property(t => t.UpdateDate)
                .HasColumnName("update_date").HasComment("更新时间");
            builder.Property(t => t.Hoted)
                .HasColumnName("hoted").HasComment("是否热门");
            builder.Property(t => t.Creamed)
                .HasColumnName("creamed").HasComment("是否精华");
            builder.Property(t => t.CreationTime).IsRequired()
                .HasColumnName("creation_time").HasComment("创建时间");

            builder.HasOne(t => t.Category).WithMany(t => t.Topics).HasForeignKey("category_id"); ;
        }
    }
}
