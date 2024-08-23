using MySelf.Zero.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace MySelf.Zero.Domain.Repositories
{
    public interface IPostsRepository : IRepository<PostsEntity, long>
    {
        Task<Tuple<int, List<PostsEntity>>> GetPostsByTopic(long topicId, int pageIndex, int pageSize);
    }

}
