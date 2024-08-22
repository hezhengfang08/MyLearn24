using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MySelf.Zero.EntityFrameworkCore.Repositories
{
    public class CategoryRepositroy : EfCoreRepository<ZeroDbContext, CategoryEntity, long>, ICategoryRepositroy
    {

        public CategoryRepositroy(IDbContextProvider<ZeroDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds)
        {
            return (await GetQueryableAsync()).Where(t => userIds.Contains(t.Id)).ToList();
        }
    }
}
