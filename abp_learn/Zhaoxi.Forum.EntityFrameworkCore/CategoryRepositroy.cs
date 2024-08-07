using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.Domain.Entities;
using Zhaoxi.Forum.Domain.Repositories;

namespace Zhaoxi.Forum.EntityFrameworkCore;

public class CategoryRepositroy : EfCoreRepository<ForumDbContext, CategoryEntity, long>, ICategoryRepositroy
{
    public CategoryRepositroy(IDbContextProvider<ForumDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds)
    {
        return (await GetQueryableAsync()).Where(t => userIds.Contains(t.Id)).ToList();
    }
}