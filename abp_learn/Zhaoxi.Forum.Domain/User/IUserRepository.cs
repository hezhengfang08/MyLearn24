using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace Zhaoxi.Forum.Domain.User
{
    public interface IUserRepository : IRepository<UserEntity, long>
    {
        Task<List<UserEntity>> GetListOfIdArrayAsync(long[] userIds);

        /// <summary>
        /// 获取用户名对象
        /// </summary>
        Task<UserEntity> GetByUserName(string userName);

        /// <summary>
        /// 验证唯一
        /// </summary>
        Task<bool> UniqueAsync(UserEntity entity);
    }
}
