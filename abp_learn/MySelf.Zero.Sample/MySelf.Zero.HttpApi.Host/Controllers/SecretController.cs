using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zero.Application.Contracts;
using MySelf.Zero.Application.Contracts.Auth;
using MySelf.Zero.Application.Contracts.User;

namespace MySelf.Zero.HttpApi.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = "Permission")]
    [Route("api/app/[controller]")]
    public class SecretController : ControllerBase
    {
        /// <summary>
        /// Jwt 服务
        /// </summary>
        private readonly IJwtAppService _jwtApp;

        /// <summary>
        /// 配置信息
        /// </summary>
        public IConfiguration _configuration { get; }

        public SecretController(IConfiguration configuration,
            IJwtAppService jwtApp)
        {
            _configuration = configuration;
            _jwtApp = jwtApp;
        }

        /// <summary>
        /// 停用 Jwt 授权数据
        /// </summary>
        /// <returns></returns>
        [HttpPost("deactivate")]
        public async Task<IActionResult> CancelAccessToken()
        {
            await _jwtApp.DeactivateCurrentAsync();
            return Ok();
        }

        /// <summary>
        /// 刷新 Jwt 授权数据
        /// </summary>
        /// <param name="dto">刷新授权用户信息</param>
        /// <returns></returns>
        [HttpPost("refresh")]
        public async Task<IActionResult> RefreshAccessTokenAsync([FromBody] SecretDto dto)
        {
            var user = new UserDto
            {
                Phone = dto.Phone,
                Email = dto.Email,
                Password = dto.Password
            };
            var jwt = await _jwtApp.RefreshAsync(dto.Token, user);

            return Ok(jwt);
        }
    }

}
