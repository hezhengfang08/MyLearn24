﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Configuration
{
    public class FollowUserConfiguration : IEntityTypeConfiguration<FollowUser>
    {
        public void Configure(EntityTypeBuilder<FollowUser> builder)
        {
            // 设置组合唯一约束
            builder
                .HasIndex(fu => new { fu.FollowerId, fu.FolloweeId })
                .IsUnique();

            // 设置关注者与关注列表之间的一对多关系
            builder
                .HasOne(fu => fu.Follower)
                .WithMany(u => u.Followees)
                .HasForeignKey(fu => fu.FollowerId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);

            // 设置被关注者与粉丝列表之间的一对多关系
            builder
                .HasOne(fu => fu.Followee)
                .WithMany(u => u.Followers)
                .HasForeignKey(fu => fu.FolloweeId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
