using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Category;

namespace Zhaoxi.Forum.EntityFrameworkCore.ModelConfigurations;

public class CategoryDbMapping : IEntityTypeConfiguration<CategoryEntity>
{
    public void Configure(EntityTypeBuilder<CategoryEntity> builder)
    {
        // Primary Key
        builder.HasKey(m => m.Id).HasName("pk_category_id");

        // Table & Column Mappings
        builder.ToTable("category");

        builder.Property(m => m.Id)
            .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.None)
            .IsRequired()
            .HasColumnName("id")
            .HasComment("主键标识");

        builder.Property(t => t.Name).IsRequired().HasMaxLength(200)
            .HasColumnName("name").HasComment("名称");
        builder.Property(t => t.Description).HasMaxLength(2000)
            .HasColumnName("description").HasComment("描叙");
        builder.Property(t => t.IsLocked).HasDefaultValue(false)
            .HasColumnName("is_locked").HasColumnType("bit(1)").HasComment("是否锁定");
        builder.Property(t => t.SortOrder).IsRequired()
            .HasColumnName("sort_order").HasComment("板块排序");
        builder.Property(t => t.Image).HasMaxLength(200)
            .HasColumnName("image").HasComment("图像");
        builder.Property(t => t.Pinyin).HasMaxLength(50)
            .HasColumnName("pinyin").HasComment("拼音");
        builder.Property(t => t.ParentCategory)
            .HasColumnName("parent_category").HasComment("父板块");
        builder.Property(t => t.Enabled).HasDefaultValue(true)
            .HasColumnName("enabled").HasColumnType("bit(1)").HasComment("是否启用");
        builder.Property(t => t.CreationTime).IsRequired()
            .HasColumnName("creation_time").HasComment("创建时间");

        builder.HasMany(t => t.Topics).WithOne(t => t.Category);
    }
}
