using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.Zero.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 是否通过
        /// </summary>
        public bool IsPass { get; set; } = false;

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        public string UserAvatar { get; set; }

        /// <summary>
        /// 是否在黑名单
        /// </summary>
        public bool? IsBlack { get; set; } = false;

        /// <summary>
        /// 拉入黑名单时间
        /// </summary>
        public DateTime? BlackTime { get; set; }

        /// <summary>
        /// 发帖数量
        /// </summary>
        public int? TopicTimes { get; set; }

        /// <summary>
        /// 关注数量
        /// </summary>
        public int? FollowTimes { get; set; }
    }
}
