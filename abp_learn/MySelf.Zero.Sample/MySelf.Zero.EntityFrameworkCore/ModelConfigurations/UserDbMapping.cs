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
    public class UserDbMapping : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            // Primary Key
            builder.HasKey(t => t.Id).HasName("pk_user_id");

            // Table & Column Mappings
            builder.ToTable("user");

            builder.Property(m => m.Id)
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .IsRequired()
                .HasColumnName("id")
                .HasComment("主键标识");

            builder.Property(t => t.Phone).HasMaxLength(20)
                .HasColumnName("phone").HasComment("手机号");
            builder.Property(t => t.Password).HasMaxLength(50)
                .HasColumnName("password").HasComment("密码");
            builder.Property(t => t.IsPass).HasDefaultValue(false)
                .HasColumnName("is_pass").HasColumnType("bit(1)").HasComment("是否通过");
            builder.Property(t => t.Email).HasMaxLength(50)
                .HasColumnName("email").HasComment("邮箱");
            builder.Property(t => t.NickName).HasMaxLength(50)
                .HasColumnName("nick_name").HasComment("昵称");
            builder.Property(t => t.UserAvatar).IsRequired(false).HasMaxLength(200)
                .HasColumnName("user_avatar").HasComment("用户头像");
            builder.Property(t => t.Sex)
                .HasColumnName("sex").HasComment("性别");
            builder.Property(t => t.FollowTimes)
                .HasColumnName("follow_times").HasComment("关注数量");
            builder.Property(t => t.IsBlack).HasDefaultValue(false)
                .HasColumnName("is_black").HasColumnType("bit(1)").HasComment("是否在黑名单");
            builder.Property(t => t.BlackTime)
                .HasColumnName("black_time").HasComment("拉入黑名单时间");
            builder.Property(t => t.TopicTimes)
                .HasColumnName("topic_times").HasComment("发帖数量");
            builder.Property(t => t.CreationTime).IsRequired()
                .HasColumnName("creation_time").HasComment("创建时间");

        }
    }

}
