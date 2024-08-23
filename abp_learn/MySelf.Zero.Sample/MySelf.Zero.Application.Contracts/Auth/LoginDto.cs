using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Application.Contracts.Auth
{
    public class LoginDto
    {
        public long UserId { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }

}
