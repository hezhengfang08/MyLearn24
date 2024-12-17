using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using MySelf.Zhihu.Core.QuestionAggregate.Entites;
using MySelf.Zhihu.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.Common.Interfaces
{
    public interface IDataQueryService
    {
        public IQueryable<AppUser> AppUsers { get; }

        public IQueryable<FollowQuestion> FollowQuestions { get; }

        public IQueryable<FollowUser> FollowUsers { get; }

        public IQueryable<Question> Questions { get; }

        public IQueryable<Answer> Answers { get; }

        public IQueryable<AnswerLike> AnswerLikes { get; }

        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class;

        Task<IList<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class;
        Task<PagedList<T>> ToPageListAsync<T>(IQueryable<T> queryable, Pagination pagination) where T : class;

        Task<bool> AnyAsync<T>(IQueryable<T> queryable) where T : class;
    }
}
