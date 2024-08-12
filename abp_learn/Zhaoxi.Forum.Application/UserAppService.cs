using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Application.Contracts.User;
using Zhaoxi.Forum.Domain.User;

namespace Zhaoxi.Forum.Application
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserRepository _userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AnyAsync()
        {
            return await _userRepository.GetCountAsync() > 0;
        }

        public async Task ImportAsync(IEnumerable<UserImportDto> importDtos)
        {
            var users = ObjectMapper.Map<IEnumerable<UserImportDto>,
                IEnumerable<UserEntity>>(importDtos);

            await _userRepository.InsertManyAsync(users);
        }
    }

}
