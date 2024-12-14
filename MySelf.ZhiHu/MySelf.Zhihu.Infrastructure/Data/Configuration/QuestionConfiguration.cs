using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Configuration
{
    public class QuestionConfiguration : IEntityTypeConfiguration<Question>
    {
        public void Configure(EntityTypeBuilder<Question> builder)
        {
            builder.Property(p => p.Title)
            .HasMaxLength(DataSchemaConstants.DefaultQuestionTitleLength)
            .IsRequired();

            builder.Property(p => p.Description)
                .HasColumnType("text");
        }
    }
}
