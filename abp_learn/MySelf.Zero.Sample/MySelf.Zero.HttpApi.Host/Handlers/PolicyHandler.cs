using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using MySelf.Zero.Application.Contracts.Auth;
using System.Security.Claims;

namespace MySelf.Zero.HttpApi.Host.Handlers
{
    public class PolicyHandler : AuthorizationHandler<PolicyRequirement>
    {
        /// <summary>
        /// 授权方式（cookie, bearer, oauth, openid）
        /// </summary>
        public IAuthenticationSchemeProvider Schemes { get; set; }

        /// <summary>
        /// jwt 服务
        /// </summary>
        private readonly IJwtAppService _jwtApp;

        public PolicyHandler(IAuthenticationSchemeProvider schemes, IJwtAppService jwtApp)
        {
            Schemes = schemes;
            _jwtApp = jwtApp;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PolicyRequirement requirement)
        {
            // Todo：获取角色、Url 对应关系
            List<Menu> list = new List<Menu> {
            new Menu
            {
                Role = Guid.Empty.ToString(),
                Url = "/api/app/topic/send-topic"
            },
            new Menu
            {
                Role = Guid.Empty.ToString(),
                Url = "/api/app/topic/reply-posts"
            },
            new Menu
            {
                Role = Guid.Empty.ToString(),
                Url = "/api/app/secret/deactivate"
            },
            new Menu
            {
                Role = Guid.Empty.ToString(),
                Url = "/api/app/secret/refresh"
            }
        };

            var httpContext = context.Resource as HttpContext;
            // 获取授权方式
            var defaultAuthenticate = await Schemes.GetDefaultAuthenticateSchemeAsync();
            if (defaultAuthenticate != null && httpContext != null)
            {
                // 验证签发的用户信息
                var result = await httpContext.AuthenticateAsync(defaultAuthenticate.Name);
                if (result.Succeeded)
                {
                    // 判断是否为已停用的 Token
                    if (!await _jwtApp.IsCurrentActiveTokenAsync())
                    {
                        context.Fail();
                        return;
                    }

                    httpContext.User = result.Principal;
                    // 判断角色与 Url 是否对应
                    var url = httpContext.Request.Path.Value.ToLower();
                    var role = httpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role).Value;
                    var menu = list.Where(x => x.Role.Equals(role) && x.Url.ToLower().Equals(url)).FirstOrDefault();
                    if (menu == null)
                    {
                        context.Fail();
                        return;
                    }

                    // 判断是否过期
                    var expiration = httpContext.User.Claims.SingleOrDefault(s => s.Type == ClaimTypes.Expiration);
                    if (DateTime.Parse(expiration.Value) >= DateTime.UtcNow)
                    {
                        context.Succeed(requirement);
                    }
                    else
                    {
                        context.Fail();
                    }
                    return;
                }
            }

            context.Fail();
        }
    }

}
