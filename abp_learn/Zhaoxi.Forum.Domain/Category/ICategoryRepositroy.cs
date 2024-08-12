
using Volo.Abp.Domain.Repositories;

namespace Zhaoxi.Forum.Domain.Category
{
    public interface ICategoryRepositroy : IRepository<CategoryEntity, long>
    {
        Task<List<CategoryEntity>> GetListOfIdArrayAsync(long[] userIds);

    }
}
