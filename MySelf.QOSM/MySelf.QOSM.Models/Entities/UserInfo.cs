using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public class UserInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; } = null!;
        public string UserPwd { get; set; } = null!;
        public string? Phone { get; set; }
        public int RoleId { get; set; }
        public DateTime CreateTime { get; set; }
        public bool UserState { get; set; }
        public bool IsDeleted { get; set; }
    }
}
