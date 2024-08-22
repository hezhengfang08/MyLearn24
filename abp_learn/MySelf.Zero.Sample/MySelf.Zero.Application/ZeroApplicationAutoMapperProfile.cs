using AutoMapper;
using MySelf.Zero.Application.Contracts;
using MySelf.Zero.Domain.Entities;
namespace MySelf.Zero.Application
{
    public class ZeroApplicationAutoMapperProfile:Profile
    {
        public ZeroApplicationAutoMapperProfile() { 
            ///添加映射关系 两边关系同时存在
            CreateMap<CategoryEntity, CategoryDto>();
            CreateMap<CategoryDto, CategoryEntity>();

            CreateMap<TopicDto, TopicEntity>();
            CreateMap<TopicEntity, TopicDto>();
        }
        
    }
}
