using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using MyRedis01.RedisConstants;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks.Dataflow;

namespace MyRedis01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly IRedisDatabase redis;
        public TagController(IRedisDatabase redis)
        {
            this.redis = redis;
        }
        [HttpGet]
        public async Task<ActionResult> GetTags(long id)
        {
            var result = await redis.SetMemberAsync(ArticleConstant.ArticleTags + id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult> CreateTags(long id, string tags)
        {
           await redis.SetAddAllAsync(ArticleConstant.ArticleTags+id, StackExchange.Redis.CommandFlags.None,tags.Split(","));
            return Ok();
           
           
        }
        [HttpDelete]
        public async Task<ActionResult> DelteTag(long id, string tag)
        {
            await redis.SetRemoveAsync(ArticleConstant.ArticleTags+id, tag);
            return Ok();    
        }

    }
}
