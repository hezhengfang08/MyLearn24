
using Volo.Abp.Domain.Repositories;
using Zhaoxi.Forum.Domain.Entities;

namespace Zhaoxi.Forum.Domain.Repositories
{
    public interface ICategoryRepositroy:IRepository<CategoryEntity, long>
    {
        Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);

    }
}
