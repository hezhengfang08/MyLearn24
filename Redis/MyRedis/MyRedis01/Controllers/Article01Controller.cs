using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyRedis01.Dtos;
using MyRedis01.Entities;
using MyRedis01.RedisConstants;
using StackExchange.Redis;
using StackExchange.Redis.Extensions.Core.Abstractions;
using System.Globalization;

namespace MyRedis01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Article01Controller : ControllerBase
    {
        private readonly IRedisDatabase redisDatabase;
        private readonly IMapper maper;
        public Article01Controller(IRedisDatabase redisDatabase, IMapper maper)
        {
            this.redisDatabase = redisDatabase;
            this.maper = maper;
        }
        /// <summary>
        /// get
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get(long id)
        {
            var article = await redisDatabase.GetAsync<Article>(ArticleConstant.ArticleData + id);
            var articleDto = maper.Map<ArticleDto>(article);
            articleDto.PageView = await redisDatabase.Database.StringIncrementAsync(ArticleConstant.ArticlePageView + id);
            return Ok(articleDto);

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateArticleDto createArticle)
        {
            var article = maper.Map<Article>(createArticle);
            article.Id = await redisDatabase.Database.StringIncrementAsync(ArticleConstant.ArticleCount);
            article.ReleaseTime = DateTime.Now.ToString(CultureInfo.CurrentCulture);
            await redisDatabase.AddAsync(ArticleConstant.ArticleData+article.Id, article);
            return CreatedAtAction("Get", article);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(long id)
        {
            await redisDatabase.RemoveAsync(ArticleConstant.ArticleData + id);
            await redisDatabase.RemoveAsync(ArticleConstant.ArticlePageView + id);
            await redisDatabase.Database.StringDecrementAsync(ArticleConstant.ArticleCount);
            return Ok();
        }
    }
}

