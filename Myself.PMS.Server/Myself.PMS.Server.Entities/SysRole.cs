using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    public class SysRole
    {
        [SugarColumn(ColumnName = "role_id", IsPrimaryKey = true)]
        public int RoleId { get; set; }

        [SugarColumn(ColumnName = "role_name")]
        public string RoleName { get; set; }

        [SugarColumn(ColumnName = "role_desc")]
        public string? RoleDesc { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }

        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(RoleMenu.RoleId))]
        public List<RoleMenu>? Menus { get; set; }

        [SugarColumn(IsIgnore = true)]
        [Navigate(NavigateType.OneToMany, nameof(RoleUser.RoleId))]
        public List<RoleUser>? Users { get; set; }
    }
}
