using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;

namespace Zhaoxi.Forum.Domain.User
{
    public class UserDomainService : DomainService
    {
        private readonly IUserRepository _userRepository;

        public UserDomainService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /// <summary>
        /// 获取用户名对象
        /// </summary>
        public async Task<UserEntity> GetByUserName(string userName)
        {
            return await _userRepository.FindAsync(u => u.Phone == userName || u.Email == userName);
        }

        /// <summary>
        /// 验证唯一
        /// </summary>
        public async Task<bool> UniqueAsync(UserEntity entity)
        {
            var queryable = await _userRepository.GetQueryableAsync();

            return queryable.Any(u => u.Phone == entity.Phone) ||
                queryable.Any(u => u.Email == entity.Email);
        }
    }
}
