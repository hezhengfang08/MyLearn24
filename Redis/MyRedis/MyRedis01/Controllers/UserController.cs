using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRedis01.RedisConstants;
using StackExchange.Redis.Extensions.Core.Abstractions;

namespace MyRedis01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRedisDatabase redis;
        public UserController(IRedisDatabase redis)
        {
            this.redis = redis;
        }
        [HttpPost]
        public async Task<ActionResult> CreateSign(long id)
        {
            var month = DateTime.Now.ToString("yyyyMM");
            var key = $"{UserConstant.UserSignIn}{id}:{month} ";
            await redis.Database.StringSetBitAsync(key, DateTime.Now.Day - 1, true);
            return Ok();

        }
        [HttpGet]
        public async Task<ActionResult> GetSign(long id, string month)
        {
            var key = $"{UserConstant.UserSignIn}{id}:{month}";
            var res = await redis.Database.StringGetBitAsync(key, DateTime.Now.Day - 1);
            return Ok(res);
        }

    }
}
