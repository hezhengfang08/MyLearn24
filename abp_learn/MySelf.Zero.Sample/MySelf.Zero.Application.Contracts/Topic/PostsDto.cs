using MySelf.Zero.Application.Contracts.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Topic
{
    public class PostsDto
    {
        public long Id { get; set; }

        public long UserId { get; set; }

        public string PostContent { get; set; }

        public string IpAddress { get; set; }

        public bool? IsRead { get; set; }

        public UserDto User { get; set; }
    }
}
