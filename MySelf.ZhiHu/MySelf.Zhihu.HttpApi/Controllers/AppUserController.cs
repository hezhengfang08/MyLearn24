using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.UseCases.AppUsers.Commands;
using MySelf.Zhihu.UseCases.AppUsers.Queries;
using MySelf.Zhihu.UseCases.AppUsers.Queries.QeurytUserInfo;

namespace MySelf.Zhihu.HttpApi.Controllers
{
   
    public class AppUserController : ApiControllerBase
    {
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await Sender.Send(new GetUserInfoQuery(id));

            return ReturnResult(result);
        }
    }
}
