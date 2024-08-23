using AutoMapper.Internal.Mappers;
using MySelf.Zero.Application.Contracts.User;
using MySelf.Zero.Domain.Entities;
using MySelf.Zero.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application
{
    public class UserAppService : ApplicationService, IUserAppService
    {
        private readonly IUserRepository userRepository;

        public UserAppService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<bool> AnyAsync()
        {
            return await userRepository.GetCountAsync() > 0;
        }

        public async Task ImportAsync(IEnumerable<UserImportDto> importDtos)
        {
            var users = ObjectMapper.Map<IEnumerable<UserImportDto>,
                IEnumerable<UserEntity>>(importDtos);

            await userRepository.InsertManyAsync(users);
        }
    }

}
