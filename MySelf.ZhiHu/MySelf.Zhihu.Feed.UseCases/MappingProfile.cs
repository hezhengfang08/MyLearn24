using MySelf.Zhihu.Feed.UseCases.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MySelf.Zhihu.Feed.UseCases
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateInboxFeedsCommand, Inbox>();

            CreateMap<CreateOutboxFeedCommand, Outbox>()
                .ForMember(
                    dest => dest.UserId,
                    opt => opt.MapFrom(src => src.SenderId));
        }
    }

}
