
using Volo.Abp.Domain.Repositories;


namespace Zhaoxi.Forum.Domain.Topic
{
    public interface ITopicRepository : IRepository<TopicEntity, long>
    {
        Task<List<TopicEntity>> GetHotTopicsAsync(long categoryId, int times = 5);

        Task<Tuple<int, List<TopicEntity>>> GetTopicByCategory(long categoryId, int pageIndex, int pageSize);
    }
}
