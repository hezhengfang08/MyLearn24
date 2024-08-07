using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.Domain.Entities;
using Zhaoxi.Forum.Domain.Repositories;

namespace Zhaoxi.Forum.EntityFrameworkCore;

public class TopicRepository : EfCoreRepository<ForumDbContext, TopicEntity, long>, ITopicRepository
{
    public TopicRepository(IDbContextProvider<ForumDbContext> dbContextProvider) : base(dbContextProvider)
    {
    }

    public async Task<List<TopicEntity>> GetHotTopicsAsync(long categoryId, int times = 5)
    {
        return (await GetQueryableAsync())
            .Where(t => t.Category.Id == categoryId && t.Hoted == true).Take(times).ToList();
    }

    public async Task<Tuple<int, List<TopicEntity>>> GetTopicByCategory(long categoryId, int pageIndex, int pageSize)
    {
        var queryable = await GetQueryableAsync();
        var result = queryable.Where(m => m.Category.Id == categoryId);
        var total = result.Count(m => m.Category.Id == categoryId);

        var list = result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

        return new Tuple<int, List<TopicEntity>>(total, list);
    }
}