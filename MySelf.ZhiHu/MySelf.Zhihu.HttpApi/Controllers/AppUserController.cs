using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.UseCases.AppUsers.Commands;
using MySelf.Zhihu.UseCases.AppUsers.Queries;


namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Route("api/appuser")]
    public class AppUserController : ApiControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Sender.Send(new GetUserInfoQuery(id));

            return ReturnResult(result);
        }

        [HttpPost("follow/question/{id:int}")]
        public async Task<IActionResult> CreateFollowQuestion(int id)
        {
            var result = await Sender.Send(new CreateFollowQuestionCommand(id));

            return ReturnResult(result);
        }

        [HttpDelete("follow/question/{id:int}")]
        public async Task<IActionResult> DeleteFollowQuestion(int id)
        {
            var result = await Sender.Send(new DeleteFollowQuestionCommand(id));

            return ReturnResult(result);
        }
        [HttpPost("follow/user/{id:int}")]
        public async Task<IActionResult> CreateFolloweeUser(int id)
        {
            var result = await Sender.Send(new CreateFolloweeUserCommand(id));

            return ReturnResult(result);
        }

        [HttpDelete("follow/user/{id:int}")]
        public async Task<IActionResult> DeletefolloweeUser(int id)
        {
            var result = await Sender.Send(new DeleteFolloweeUserCommand(id));

            return ReturnResult(result);
        }

    }
}
