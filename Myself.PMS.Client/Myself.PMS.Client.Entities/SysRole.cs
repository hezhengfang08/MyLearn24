using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class SysRole
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string? RoleDesc { get; set; }
        public int State { get; set; }
        public List<RoleMenu>? Menus { get; set; }
        public List<RoleUser>? Users { get; set; }
    }
}
