
using Magicodes.ExporterAndImporter.Core;
using System.ComponentModel.DataAnnotations;


namespace MySelf.Zero.Application.Contracts.User
{
    public class UserImportDto
    {
        /// <summary>
        /// 手机号
        /// </summary>
        [ImporterHeader(Name = "手机号")]
        [MaxLength(18, ErrorMessage = "手机号超出最大限制,请修改!")]
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [ImporterHeader(Name = "密码")]
        [MaxLength(20, ErrorMessage = "密码超出最大限制,请修改!")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [ImporterHeader(Name = "昵称")]
        [MaxLength(50, ErrorMessage = "昵称超出最大限制,请修改!")]
        public string NickName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [ImporterHeader(Name = "邮箱")]
        [MaxLength(50, ErrorMessage = "邮箱超出最大限制,请修改!")]
        public string Email { get; set; }

        /// <summary>
        /// 用户头像
        /// </summary>
        [ImporterHeader(Name = "用户头像")]
        public string UserAvatar { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        [ImporterHeader(Name = "性别")]
        public string Sex { get; set; }
    }
}
