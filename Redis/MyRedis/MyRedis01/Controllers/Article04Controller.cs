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
    public class Article04Controller : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IRedisDatabase redis;
        public Article04Controller(IMapper mapper, IRedisDatabase redis)
        {
            this.mapper = mapper;
            this.redis = redis;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetAsync(long id)
        {

            var article = await redis.HashGetAllAsync<object>(ArticleConstant.ArticleData + id);
            var pageView = await redis.SortedSetAddIncrementAsync(ArticleConstant.ArticlesPageView, id, 1);
            article.Add("PageView", pageView);
            return Ok(article);

        }
        [HttpPost]
        public async Task<ActionResult> Create(CreateArticleDto createArticle)
        {
            var article = mapper.Map<Article>(createArticle);
            article.Id = await redis.Database.StringIncrementAsync(ArticleConstant.ArticleCount);
            article.ReleaseTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            var hashKey = ArticleConstant.ArticleData + article.Id;
            var articleDic = mapper.Map<Dictionary<string, object>>(article);
            await redis.HashSetAsync(hashKey, articleDic);
            await redis.ListAddToLeftAsync(ArticleConstant.ArticleList, article.Id);
            return CreatedAtAction("Get", new { id = article.Id, article });

        }
        [HttpDelete]
        public async Task<ActionResult> Delete(long id)
        {
            {
                await redis.RemoveAsync(ArticleConstant.ArticleData + id);
                await redis.RemoveAsync(ArticleConstant.ArticlePageView + id);
                await redis.Database.ListRemoveAsync(ArticleConstant.ArticleList, id);
                await redis.Database.StringDecrementAsync(ArticleConstant.ArticleCount);
                await redis.SetRemoveAsync(ArticleConstant.ArticleTags, id);
                await redis.SortedSetRemoveAsync(ArticleConstant.ArticlesPageView, id);

                return Ok();

            }
        }
        [HttpGet]
        public async Task<ActionResult> GetArticles(ArticleParameter parameter)
        {
            var start = (parameter.PageNumber - 1) * parameter.PageSize + 1;
            var end = parameter.PageNumber * parameter.PageSize;
            var ids = await redis.SortedSetRangeByRankWithScoresAsync<long>(ArticleConstant.ArticlesPageView, start, end);
            var result = new List<IDictionary<string, object>>();
            foreach (var id in ids)
            {
                var temp = await redis.HashGetAllAsync<object?>(ArticleConstant.ArticleData + id);
                result.Add(temp);
            }
            return Ok(result);
        }
    }
}
