using AutoMapper;
using MySelf.Zhihu.Core.AppUserAggregate.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zhihu.UseCases.AppUsers
{

    public record AppUserDto(int Id, string Nickname);

    public record TokenDto(string AccessToken);


    public class DtoMapping: Profile
    {
        public DtoMapping()
        {
            CreateMap<AppUser, AppUserDto>();
        }
    }
}
