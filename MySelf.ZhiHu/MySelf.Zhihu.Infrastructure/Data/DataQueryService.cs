using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data
{
    public class DataQueryService(AppDbContext dbContext) : IDataQueryService
    {
        public IQueryable<AppUser> AppUsers => dbContext.AppUsers;

        public IQueryable<FollowQuestion> FollowQuestions => dbContext.FollowQuestions;

        public IQueryable<FollowUser> FollowUsers => dbContext.FollowUsers;

        public IQueryable<Question> Questions => dbContext.Questions;

        public IQueryable<Answer> Answers => dbContext.Answers;

        public IQueryable<AnswerLike> AnswerLikes => dbContext.AnswerLikes;

        public async Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class
        {
            return await queryable.FirstOrDefaultAsync();
        }

        public async Task<IList<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class
        {
            return await queryable.ToListAsync();
        }
    }
}
