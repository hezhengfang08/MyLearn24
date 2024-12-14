using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySelf.Zhihu.UseCases.Common.Interfaces;
using MySelf.Zhihu.SharedKernel.Result;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace MySelf.Zhihu.Infrastructure.Identity
{
    public class IdentityService(UserManager<IdentityUser> userManager, IOptions<JwtSettings> jwtSettings)
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<Result<int>> CreateUserAsync(string username, string password)
        {
            var user = new IdentityUser
            {
                UserName = username,
                Email = username
            };

            var identityResult = await userManager.CreateAsync(user, password);

            return identityResult.Succeeded
                ? Result.Success(user.Id)
                : Result.Failure(identityResult.Errors.Select(e => e.Description));
        }
        public async Task<Result<string>> GetAccessTokenAsync(string username, string password)
        {
            // 验证用户名和密码
            var user = await userManager.FindByNameAsync(username);
            if (user is null || !await userManager.CheckPasswordAsync(user, password)) return Result.Unauthorized();

            // 创建 JWT
            var jwt = new JwtSecurityToken(
                jwtSettings.Value.Issuer,
                jwtSettings.Value.Audience,
                new[]
                {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName!)
                },
                expires: DateTime.Now.AddMinutes(jwtSettings.Value.AccessTokenExpirationMinutes),
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Value.Secret!)),
                    SecurityAlgorithms.HmacSha256)
            );
            // 生成 JWT 字符串
            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token is null ? Result.Failure() : Result.Success(token);
        }
    }
}
