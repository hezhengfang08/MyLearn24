using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Repositories
{
    public class EfReadRepository<T>(AppDbContext dbContext) : IReadRepository<T> where T : class, IAggregateRoot
    {
        public async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default) where TKey : notnull
        {
            return await dbContext.Set<T>().FindAsync(new object[] { id }, cancellationToken);
        }

        public async Task<int> GetCountAsync(Expression<Func<T, bool>> express, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().Where(express).CountAsync(cancellationToken);
        }

        public async Task<List<T>> GetListAsync(Expression<Func<T, bool>> express, CancellationToken cancellationToken = default)
        {
            return await dbContext.Set<T>().Where(express).ToListAsync(cancellationToken);
        }

        public IQueryable<T> GetQuaryable()
        {
            return dbContext.Set<T>().AsQueryable(); throw new NotImplementedException();
        }
    }
}
