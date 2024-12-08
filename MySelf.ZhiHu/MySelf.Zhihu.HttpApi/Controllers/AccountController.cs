using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.UseCases.AppUsers.Commands;
using MySelf.Zhihu.UseCases.AppUsers.Queries;

namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ApiControllerBase
    {
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterUserCommand registerUser)
        {
            var result = await Sender.Send(registerUser);

            return ReturnResult(result);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserQuery loginUser)
        {
            var result = await Sender.Send(loginUser);

            return ReturnResult(result);
        }
    }
}
