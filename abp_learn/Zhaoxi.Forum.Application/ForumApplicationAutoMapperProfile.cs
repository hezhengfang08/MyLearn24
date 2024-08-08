using AutoMapper;
using Zhaoxi.Forum.Application.Contracts;
using Zhaoxi.Forum.Domain;
using Zhaoxi.Forum.Domain.Entities;

namespace Zhaoxi.Forum.Application;

public class ForumApplicationAutoMapperProfile : Profile
{
    public ForumApplicationAutoMapperProfile()
    {
        CreateMap<CategoryImportDto, CategoryEntity>()
           .ForMember(dest => dest.Id, options => options.MapFrom(src => src.No));

        CreateMap<TopicImportDto, TopicEntity>()
            .ForMember(dest => dest.TopicName, options => options.MapFrom(src => src.Title))
            .ForMember(dest => dest.TopicContent, options => options.MapFrom(src => src.Content));

        CreateMap<CategoryEntity, CategoryDto>();
        CreateMap<CategoryDto, CategoryEntity>();

        CreateMap<TopicDto, TopicEntity>();
        CreateMap<TopicEntity, TopicDto>();

        //CreateMap<RegistInput, UserEntity>();
        //CreateMap<UserEntity, RegistInput>();

        //CreateMap<LoginInput, UserEntity>();
        //CreateMap<UserEntity, LoginInput>();
    }
}