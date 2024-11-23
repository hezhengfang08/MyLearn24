using MySelf.Zero.Domain.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace MySelf.Zero.Application.Contracts.Auth
{
    public interface IAuthAppService : IApplicationService
    {
        /// <summary>
        /// 注册
        /// </summary>
        Task<ApiResponse<LoginDto>> RegisterAsync(RegistInput input);

        /// <summary>
        /// 登录
        /// </summary>
        Task<ApiResponse<LoginDto>> LoginAsync(LoginInput input);
    }
}
