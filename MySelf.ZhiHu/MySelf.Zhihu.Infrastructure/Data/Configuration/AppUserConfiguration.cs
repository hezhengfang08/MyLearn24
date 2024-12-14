using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Configuration
{
    internal class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(p => p.Id)
           .ValueGeneratedNever(); //获取权限中心注册的用户ID，不能自动生成

            builder.Property(p => p.Nickname)
                .HasMaxLength(DataSchemaConstants.DefaultAppUserNickNameLength);

            builder.Property(p => p.Bio)
                .HasMaxLength(DataSchemaConstants.DefaultAppUserBioLength);
        }
    }
}
