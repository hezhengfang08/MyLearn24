using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Feed.Core.Entites;
using MySelf.Zhihu.Feed.Infrastructure.Data.Contexts;
using MySelf.Zhihu.SharedKernel.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data
{
    public class DataQueryService(FeedDbContext dbContext) : IDataQueryService
    {
        public IQueryable<Inbox> Inbox => dbContext.Inbox.AsNoTracking();

        public IQueryable<Outbox> Outbox => dbContext.Outbox.AsNoTracking();

        public async Task<T?> FirstOrDefaultAsync<T>(IQueryable<T> queryable) where T : class
        {
            return await queryable.AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task<List<T>> ToListAsync<T>(IQueryable<T> queryable) where T : class
        {
            return await queryable.AsNoTracking().ToListAsync();
        }

        public async Task<PagedList<T>?> ToPageListAsync<T>(IQueryable<T> queryable, Pagination pagination) where T : class
        {
            var count = queryable.Count();
            var items = await queryable
                .Skip((pagination.PageNumber - 1) * pagination.PageSize)
                .Take(pagination.PageSize)
                .AsNoTracking()
                .ToListAsync();
            return items.Count == 0 ? null : new PagedList<T>(items, count, pagination);
        }

        public async Task<bool> AnyAsync<T>(IQueryable<T> queryable) where T : class
        {
            return await queryable.AsNoTracking().AnyAsync();
        }
    }

}
