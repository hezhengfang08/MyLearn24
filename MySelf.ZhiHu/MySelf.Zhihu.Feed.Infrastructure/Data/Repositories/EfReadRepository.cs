﻿using Microsoft.EntityFrameworkCore;
using MySelf.Zhihu.Feed.Infrastructure.Data.Contexts;
using MySelf.Zhihu.SharedKernel.Domain;
using MySelf.Zhihu.SharedKernel.Repositoy;
using MySelf.Zhihu.SharedKernel.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.Feed.Infrastructure.Data.Repositories
{
    public class EfReadRepository<T>(FeedDbContext dbContext) : IReadRepository<T> where T : class, IEntity
    {
        protected readonly DbSet<T> DbSet = dbContext.Set<T>();

        public async Task<T?> GetByIdAsync<TKey>(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbSet.FindAsync(id, cancellationToken);
        }

        public async Task<List<T>> GetListAsync(ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(DbSet, specification).ToListAsync(cancellationToken);
        }

        public async Task<T?> GetSingleOrDefaultAsync(ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(DbSet, specification).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<int> CountAsync(ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(DbSet, specification).CountAsync(cancellationToken);
        }

        public async Task<bool> AnyAsync(ISpecification<T>? specification = null,
            CancellationToken cancellationToken = default)
        {
            return await SpecificationEvaluator.GetQuery(DbSet, specification).AnyAsync(cancellationToken);
        }
    }

}