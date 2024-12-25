using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.Feed.UseCases.Queries;
using MySelf.Zhihu.SharedKernel.Paging;

namespace MySelf.Zhihu.Feed.HttpApi.Controllers
{
    [Route("api/feed")]
    [ApiController]
    public class FeedController(ISender sender) : ControllerBase
    {
        [HttpGet("userid:int")]
        public async Task<IActionResult> GetList(int userid, [FromQuery] Pagination pagination)
        {
            var result = await sender.Send(new GetInboxFeedsQuery(userid, pagination));

            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
    }
}
