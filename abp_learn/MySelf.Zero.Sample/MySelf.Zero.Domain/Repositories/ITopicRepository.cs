using MySelf.Zero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MySelf.Zero.Domain.Repositories
{
    public interface ITopicRepository : IRepository<TopicEntity, long>
    {
        Task<List<TopicEntity>> GetHotTopicsAsync(long categoryId, int times = 5);

        Task<Tuple<int, List<TopicEntity>>> GetTopicByCategory(long categoryId, int pageIndex, int pageSize);
    }
}
