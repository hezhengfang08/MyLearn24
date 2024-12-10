using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.HttpApi.Models;
using MySelf.Zhihu.Infrastructure.Identity;
using MySelf.Zhihu.UseCases.AppUsers.Commands;
using System.Security.Claims;

namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Route("/identity")]

    public class IdentityController(IdentityService identityService) : ApiControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterRequest register)
        {
            var identityResult = await identityService.CreateUserAsync(register.Username, register.Password);

            if (!identityResult.IsSuccess) return ReturnResult(identityResult);

            var result = await Sender.Send(new CreateAppUserCommand(identityResult.Value));

            return ReturnResult(result);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginRequest login)
        {
            var result = await identityService.GetAccessTokenAsync(login.Username, login.Password);

            if (!result.IsSuccess) return ReturnResult(result);

            return Ok(new
            {
                AccessToken = result.Value
            });
        }

        [HttpPost("test")]
        public IActionResult Test()
        {
            return Ok(new
            {
                Username = User.FindFirstValue(ClaimTypes.Name)
            });
        }
    }
}
