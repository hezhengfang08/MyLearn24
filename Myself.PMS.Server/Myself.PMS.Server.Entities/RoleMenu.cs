using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable(TableName = "role_menu")]
    public class RoleMenu
    {
        [SugarColumn(ColumnName = "role_id", IsPrimaryKey = true)]
        public int RoleId { get; set; }
        [SugarColumn(ColumnName = "menu_id", IsPrimaryKey = true)]
        public int MenuId { get; set; }
    }
}
