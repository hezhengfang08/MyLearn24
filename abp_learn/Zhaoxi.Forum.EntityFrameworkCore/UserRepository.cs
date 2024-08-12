using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Zhaoxi.Forum.Domain.User;

namespace Zhaoxi.Forum.EntityFrameworkCore
{
    public class UserRepository : EfCoreRepository<ForumDbContext, UserEntity, long>, IUserRepository
    {
        public UserRepository(IDbContextProvider<ForumDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<List<UserEntity>> GetListOfIdArrayAsync(long[] userIds)
        {
            return (await GetQueryableAsync()).Where(t => userIds.Contains(t.Id)).ToList();
        }

        /// <summary>
        /// 获取用户名对象
        /// </summary>
        public async Task<UserEntity> GetByUserName(string userName)
        {
            return await FindAsync(u => u.Phone == userName || u.Email == userName);
        }

        /// <summary>
        /// 验证唯一
        /// </summary>
        public async Task<bool> UniqueAsync(UserEntity entity)
        {
            var queryable = await GetQueryableAsync();

            return queryable.Any(u => u.Phone == entity.Phone) ||
                queryable.Any(u => u.Email == entity.Email);
        }
    }

}
