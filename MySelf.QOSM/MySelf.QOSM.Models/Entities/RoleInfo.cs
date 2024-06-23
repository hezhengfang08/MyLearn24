using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class RoleInfo
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!;
        public bool IsAdmin { get; set; }
        public string? Remark { get; set; }
        public bool IsDeleted { get; set; }
        public List<MenuInfo> Menus { get; set; } = new List<MenuInfo>();
        public List<UserInfo> Users { get; set; } = new List<UserInfo>();
    }
}

