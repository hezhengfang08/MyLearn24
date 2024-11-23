using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using MySelf.Zero.Application.Contracts.Auth;
using MySelf.Zero.Application.Contracts.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MySelf.Zero.Application
{
    public class JwtAppService : IJwtAppService
    {
        // 已授权的 Token 信息集合
        private static ISet<JwtAuthorizationDto> _tokens = new HashSet<JwtAuthorizationDto>();
        // 分布式缓存
        private readonly IDistributedCache _cache;
        // 配置信息
        private readonly IConfiguration _configuration;
        // 获取 HTTP 请求上下文
        private readonly IHttpContextAccessor _httpContextAccessor;

        public JwtAppService(IDistributedCache cache,
            IHttpContextAccessor httpContextAccessor,
            IConfiguration configuration)
        {
            _cache = cache;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }

        /// <summary>
        /// 新增 Jwt token
        /// </summary>
        /// <param name="dto">用户信息数据传输对象</param>
        /// <returns></returns>
        public JwtAuthorizationDto Create(UserDto user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));

            DateTime authTime = DateTime.UtcNow;
            DateTime expiresAt = authTime.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]));

            // 将用户信息添加到 Claim 中
            var identity = new ClaimsIdentity(JwtBearerDefaults.AuthenticationScheme);
            var userName = !string.IsNullOrEmpty(user.Phone) ? user.Phone : user.Email;
            IEnumerable<Claim> claims = new Claim[] {
            new Claim(ClaimTypes.Name, userName),
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Role, Guid.Empty.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Expiration, expiresAt.ToString())
        };
            identity.AddClaims(claims);

            // 签发一个加密后的用户信息凭证，用来标识用户的身份
            _httpContextAccessor.HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),//创建声明信息
                Issuer = _configuration["Jwt:Issuer"],//Jwt token 的签发者
                Audience = _configuration["Jwt:Audience"],//Jwt token 的接收者
                Expires = expiresAt,//过期时间
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256)//创建 token
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            // 存储 Token 信息
            var jwt = new JwtAuthorizationDto
            {
                UserId = user.UserId,
                Token = tokenHandler.WriteToken(token),
                Auths = new DateTimeOffset(authTime).ToUnixTimeSeconds(),
                Expires = new DateTimeOffset(expiresAt).ToUnixTimeSeconds(),
                Success = true
            };

            _tokens.Add(jwt);

            return jwt;
        }

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <returns></returns>
        public async Task DeactivateAsync(string token)
        {
            await _cache.SetStringAsync(GetKey(token),
                            " ", new DistributedCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow =
                                    TimeSpan.FromMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"]))
                            });
        }

        /// <summary>
        /// 停用当前 Token
        /// </summary>
        /// <returns></returns>
        public async Task DeactivateCurrentAsync()
        {
            await DeactivateAsync(GetCurrentAsync());
        }

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        public async Task<bool> IsActiveAsync(string token)
        {
            return await _cache.GetStringAsync(GetKey(token)) == null;
        }

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        public async Task<bool> IsCurrentActiveTokenAsync()
        {
            return await IsActiveAsync(GetCurrentAsync());
        }

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="dto">用户信息数据传输对象</param>
        /// <returns></returns>
        public async Task<JwtAuthorizationDto> RefreshAsync(string token, UserDto dto)
        {
            var jwtOld = GetExistenceToken(token);
            if (jwtOld == null)
            {
                return new JwtAuthorizationDto()
                {
                    Token = "未获取到当前 Token 信息",
                    Success = false
                };
            }

            var jwt = Create(dto);

            // 停用修改前的 Token 信息
            await DeactivateCurrentAsync();

            return jwt;
        }

        /// <summary>
        /// 获取 HTTP 请求的 Token 值
        /// </summary>
        /// <returns></returns>
        private string GetCurrentAsync()
        {
            // http header
            var authorizationHeader = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
            // token
            return authorizationHeader == StringValues.Empty
                ? string.Empty
                : authorizationHeader.Single().Split(" ").Last();// bearer tokenvalue
        }

        /// <summary>
        /// 设置缓存中过期 Token 值的 key
        /// </summary>
        private static string GetKey(string token)
            => $"deactivated token:{token}";

        /// <summary>
        /// 判断是否存在当前 Token
        /// </summary>
        private JwtAuthorizationDto GetExistenceToken(string token)
            => _tokens.SingleOrDefault(x => x.Token == token);
    }

}
