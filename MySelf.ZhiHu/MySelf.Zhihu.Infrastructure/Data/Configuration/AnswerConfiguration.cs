using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;


namespace MySelf.Zhihu.Infrastructure.Data.Configuration
{
    internal class AnswerConfiguration : IEntityTypeConfiguration<Answer>
    {
        public void Configure(EntityTypeBuilder<Answer> builder)
        {
            builder.Property(p => p.Content)
                .HasColumnType("text")
                .IsRequired();

            // 设置问题与回答列表之间的一对多关系
            builder.HasOne(a => a.Question)
                .WithMany(q => q.Answers)
                .HasForeignKey(a => a.QuestionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
