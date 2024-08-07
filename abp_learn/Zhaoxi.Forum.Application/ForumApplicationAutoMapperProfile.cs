using AutoMapper;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Domain.Entities;

namespace Zhaoxi.Forum.Application;

public class ForumApplicationAutoMapperProfile : Profile
{
    public ForumApplicationAutoMapperProfile()
    {
        //类型转换基础架构设施
        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CategoryDto, CategoryEntity>();

        CreateMap<TopicDto, TopicEntity>();
        CreateMap<TopicEntity, TopicDto>();

        CreateMap<CategoryImportDto, CategoryEntity>();
        CreateMap<CategoryEntity, CategoryImportDto>();


        CreateMap<TopicImportDto, TopicEntity>();
        CreateMap<TopicEntity, TopicImportDto>();
    }
}