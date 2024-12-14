using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.SharedKernel.Domain;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.SharedKernel.Specification;



namespace MySelf.Zhihu.Infrastructure.Data.Repositories
{
    public class EfRepository<T>(AppDbContext dbContext) : EfReadRepository<T>(dbContext), IRepository<T>
       where T : class, IEntity, IAggregateRoot
    {
        private readonly AppDbContext _dbContext = dbContext;

        public T Add(T entity)
        {
            DbSet.Add(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            DbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public async Task<int> BatchDeleteAsync(ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(DbSet, specification).ExecuteDeleteAsync(cancellationToken);
        }

        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

}
