using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Zhaoxi.Forum.Application.Contracts.Auth;
using Zhaoxi.Forum.Application.Contracts.User;
using Zhaoxi.Forum.Application.Validator;
using Zhaoxi.Forum.Domain.Shared;
using Zhaoxi.Forum.Domain.User;

namespace Zhaoxi.Forum.Application
{
    public class AuthAppService : ApplicationService, IAuthAppService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtAppService _jwtAppService;
        private readonly UserDomainService _userDomainService;

        public AuthAppService(IUserRepository userRepository,
            IJwtAppService jwtAppService,
            UserDomainService userDomainService)
        {
            _userRepository = userRepository;
            _jwtAppService = jwtAppService;
            _userDomainService = userDomainService;
        }

        public async Task<ApiResponse<LoginDto>> LoginAsync(LoginInput input)
        {
            var response = new ApiResponse<LoginDto>();
            var isValid = ValidatorMapper.LoginInput.CustomValidate(input, out IEnumerable<string> errors);
            if (!isValid)
            {
                response.IsFailed(string.Join(';', errors));

                return response;
            }

            var userName = input.UserName;
            var user = await _userDomainService.GetByUserName(userName);
            if (user != null)
            {
                var isValida = user.Password == input.Password;
                if (isValida)
                {
                    var userDto = ObjectMapper.Map<UserEntity, UserDto>(user);
                    var jwt = _jwtAppService.Create(userDto);
                    var loginDto = ObjectMapper.Map<UserDto, LoginDto>(userDto);
                    loginDto.Token = jwt.Token;

                    response.Result = loginDto;

                    response.IsSuccess("ok");
                }
                else
                {
                    response.IsFailed("密码错误！");
                }
            }
            else
            {
                response.IsFailed("手机号或者邮箱错误！");
            }

            return response;
        }

        public async Task<ApiResponse<LoginDto>> RegisterAsync(RegistInput input)
        {
            var response = new ApiResponse<LoginDto>();
            var isValid = ValidatorMapper.RegistInput.CustomValidate(input, out IEnumerable<string> errors);
            if (!isValid)
            {
                response.IsFailed(string.Join(';', errors));

                return response;
            }

            var user = ObjectMapper.Map<RegistInput, UserEntity>(input);
            var exists = await _userDomainService.UniqueAsync(user);
            if (exists)
            {
                response.IsFailed("电话或邮箱不唯一，请重新选择！");

                return response;
            }

            var databaseUser = await _userRepository.InsertAsync(user);
            if (databaseUser != null)
            {
                var userDto = ObjectMapper.Map<UserEntity, UserDto>(databaseUser);
                var jwt = _jwtAppService.Create(userDto);
                var loginDto = ObjectMapper.Map<UserDto, LoginDto>(userDto);
                loginDto.Token = jwt.Token;

                response.Result = loginDto;
                response.IsSuccess("ok");
            }
            else
            {
                response.IsFailed("注册失败！");
            }

            return response;
        }
    }

}
