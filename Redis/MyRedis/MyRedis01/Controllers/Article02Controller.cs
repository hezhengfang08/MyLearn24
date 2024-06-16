using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRedis01.Dtos;
using MyRedis01.Entities;
using MyRedis01.RedisConstants;
using MyRedis01.RequestFeatures;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Globalization;

namespace MyRedis01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Article02Controller : ControllerBase
    {
        private readonly IRedisDatabase redisDatabase;
        private readonly IMapper  mapper;
        public Article02Controller(IRedisDatabase redisDatabase, IMapper mapper)
        {
            this.redisDatabase = redisDatabase;
            this.mapper = mapper;

        }
        [HttpGet("{id}")]
        public async  Task<ActionResult> Get(long id)
        {
            var article = await redisDatabase.HashGetAllAsync<object>(ArticleConstant.ArticleData + id);
            var pageView = await redisDatabase.Database.StringIncrementAsync(ArticleConstant.ArticlePageView + id);
            article.Add("PageView", pageView);
            return Ok(article);

        }
        [HttpPost]
        public async Task<ActionResult > Create( CreateArticleDto createArticle)
        {
            var article = mapper.Map<Article>(createArticle);
            article.Id = await redisDatabase.Database.StringIncrementAsync(ArticleConstant.ArticleCount);
            article.ReleaseTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            var hashKey = ArticleConstant.ArticleData+article.Id;
            var articleDic = mapper.Map<Dictionary<string, object>>(article);
            await redisDatabase.HashSetAsync(hashKey, articleDic);
            return CreatedAtAction("Get", new {id=article.Id}, article);
        }
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            await redisDatabase.RemoveAsync(ArticleConstant.ArticleData + id);
            await redisDatabase.RemoveAsync(ArticleConstant.ArticlePageView + id);
            await redisDatabase.Database.StringDecrementAsync(ArticleConstant.ArticleCount);
            return Ok();
        }
        [HttpGet]
        public async Task<ActionResult> GetArticles([FromQuery] ArticleParameter parameter)
        {
            var starIndex = (parameter.PageNumber - 1) * parameter.PageSize + 1;
            var endIndex = parameter.PageNumber*parameter.PageSize;
            var result = new List<Dictionary<string, object>>();
            for(var i = starIndex; i <= endIndex; i++)
            {
                var article = new Dictionary<string , object>();
                var title = await redisDatabase.HashGetAsync<object>(ArticleConstant.ArticleData + i, "Title");
                article.Add("id", i);
                article.Add("title", title);
                result.Add(article);
            }

            return Ok(result);

        }
    }
}
