using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySelf.Zhihu.HttpApi.Infrastructure;
using MySelf.Zhihu.SharedKernel.Paging;
using MySelf.Zhihu.UseCases.Searches.Queries;
using Newtonsoft.Json;

namespace MySelf.Zhihu.HttpApi.Controllers
{
    [Microsoft.AspNetCore.Components.Route("api/search")]
    public class SearchController : ApiControllerBase
    {
        [HttpGet("content/{query}")]
        public async Task<IActionResult> GetContent(string query, [FromQuery] Pagination pagination)
        {
            var result = await Sender.Send(new GetContentQuery(query, pagination));
            Response.Headers.Append("Pagination", JsonConvert.SerializeObject(result.Value?.MetaData));
            return ReturnResult(result);
        }

        [HttpGet("user/{query}")]
        public async Task<IActionResult> GetUser(string query, [FromQuery] Pagination pagination)
        {
            var result = await Sender.Send(new GetAppUserQuery(query, pagination));
            Response.Headers.Append("Pagination", JsonConvert.SerializeObject(result.Value?.MetaData));
            return ReturnResult(result);
        }
    }

}
