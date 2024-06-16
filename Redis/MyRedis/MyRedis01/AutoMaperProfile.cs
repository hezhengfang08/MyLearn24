using AutoMapper;
using MyRedis01.Dtos;
using MyRedis01.Entities;

namespace MyRedis01
{
    public class AutoMaperProfile:Profile
    {
        public AutoMaperProfile() {
            CreateMap<CreateArticleDto, Article>();
            CreateMap<Article, ArticleDto>();
            CreateMap<Article, Dictionary<string, object>>().ConstructUsing(article => new Dictionary<string, object>
            {

                    {"id",article.Id},
                    {"Title",article.Title},
                    {"Content",article.Content},
                    {"Author",article.Author},
                    {"ReleaseTime",article.ReleaseTime}
            });
        }
    }
}
