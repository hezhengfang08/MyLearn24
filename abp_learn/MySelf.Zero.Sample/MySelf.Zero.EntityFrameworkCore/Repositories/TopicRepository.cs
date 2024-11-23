using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace MySelf.Zero.EntityFrameworkCore.Repositories
{
    public class TopicRepository : EfCoreRepository<ZeroDbContext, TopicEntity, long>, ITopicRepository
    {
        public TopicRepository(IDbContextProvider<ZeroDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        public async Task<List<TopicEntity>> GetHotTopicsAsync(long categoryId, int times = 5)
        {
            return (await GetQueryableAsync())
                .Where(t => t.Category.Id == categoryId && t.Hoted == true).Take(times).ToList();
        }
        //和结构差不多的东西
        public async Task<Tuple<int, List<TopicEntity>>> GetTopicByCategory(long categoryId, int pageIndex, int pageSize)
        {
           
            var queryable = await GetQueryableAsync();
            var result = queryable.Where(m => m.Category.Id == categoryId);
            var total = result.Count(m => m.Category.Id == categoryId);

            var list = result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return new Tuple<int, List<TopicEntity>>(total, list);
        }
    }
}
