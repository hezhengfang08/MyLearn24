using MySelf.Zhihu.Core.Data;
using MySelf.Zhihu.SharedKernel.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Infrastructure.Data.Repositories
{
    public class EfRepository<T>(AppDbContext dbContext) : EfReadRepository<T>(dbContext), IRepository<T>
        where T : class, IAggregateRoot
    {
        public T Add(T entity)
        {
            dbContext.Set<T>().Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            dbContext.Set<T>().Update(entity);
        }

        public void Delete(T entity)
        {
            dbContext.Set<T>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
