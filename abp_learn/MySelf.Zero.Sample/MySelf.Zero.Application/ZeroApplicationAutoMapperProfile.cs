using AutoMapper;
using MySelf.Zero.Application.Contracts.Auth;
using MySelf.Zero.Application.Contracts.Category;
using MySelf.Zero.Application.Contracts.Topic;
using MySelf.Zero.Application.Contracts.User;
using MySelf.Zero.Domain.Entities;
namespace MySelf.Zero.Application
{
    public class ZeroApplicationAutoMapperProfile:Profile
    {
        public ZeroApplicationAutoMapperProfile() {
            CreateMap<CategoryImportDto, CategoryEntity>()
            .ForMember(dest => dest.Id, options => options.MapFrom(src => src.No));

            CreateMap<TopicImportDto, TopicEntity>()
                .ForMember(dest => dest.TopicName, options => options.MapFrom(src => src.Title))
                .ForMember(dest => dest.TopicContent, options => options.MapFrom(src => src.Content));

            CreateMap<UserImportDto, UserEntity>()
                .ForMember(dest => dest.Sex, options => options.MapFrom(src => src.Sex == "女" ? 0 : 1));

            CreateMap<RegistInput, UserEntity>();
            //.ForMember(dest => dest.UserAvatar, options => options.MapFrom(src => src.Avatar));
            CreateMap<UserEntity, RegistInput>();
            //.ForMember(dest => dest.Avatar, options => options.MapFrom(src => src.UserAvatar));

            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();

            CreateMap<TopicDto, TopicEntity>();
            CreateMap<TopicEntity, TopicDto>();

            CreateMap<PostsEntity, PostsDto>();
            CreateMap<PostsDto, PostsEntity>();

            CreateMap<UserEntity, UserDto>()
                .ForMember(dest => dest.UserId, options => options.MapFrom(src => src.Id));
            CreateMap<UserDto, LoginDto>();
        }
        
    }
}
