using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityUserContext<IdentityUser, int>(options)
    {
        public DbSet<Question> Questions => Set<Question>();

        public DbSet<Answer> Answers => Set<Answer>();

        public DbSet<AnswerLike> AnswerLikes => Set<AnswerLike>();

        public DbSet<AppUser> AppUsers => Set<AppUser>();

        public DbSet<FollowUser> FollowUsers => Set<FollowUser>();

        public DbSet<FollowQuestion> FollowQuestions => Set<FollowQuestion>();
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}

