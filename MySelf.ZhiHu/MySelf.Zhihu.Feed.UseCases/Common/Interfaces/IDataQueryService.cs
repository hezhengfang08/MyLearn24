using MySelf.Zhihu.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.UseCases.Common.Interfaces
{
    public interface IDataQueryService
    {
        public IQueryable<Inbox> Inbox { get; }

        public IQueryable<Outbox> Outbox { get; }

        Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class;

        Task<List<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class;

        Task<PagedList<T>?> ToPageListAsync<T>(IQueryable<T> queryable, Pagination pagination) where T : class;

        Task<bool> AnyAsync<T>(IQueryable<T> queryable) where T : class;
    }

}
