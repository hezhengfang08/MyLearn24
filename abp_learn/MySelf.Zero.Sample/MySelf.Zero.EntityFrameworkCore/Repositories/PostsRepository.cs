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
    public class PostsRepository : EfCoreRepository<ZeroDbContext, PostsEntity, long>, IPostsRepository
    {
        public PostsRepository(IDbContextProvider<ZeroDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<Tuple<int, List<PostsEntity>>> GetPostsByTopic(long topicId, int pageIndex, int pageSize)
        {
            var queryable = await GetQueryableAsync();
            var result = queryable.Where(m => m.Topic.Id == topicId);
            var total = result.Count(m => m.Topic.Id == topicId);

            var list = result.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();

            return new Tuple<int, List<PostsEntity>>(total, list);
        }
    }

}
