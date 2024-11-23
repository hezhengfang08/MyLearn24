using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.PMS.Server.Entities
{
    [SugarTable(TableName= "role_user")]
    public class RoleUser
    {
        [SugarColumn(ColumnName = "role_id")]
        public int RoleId { get; set; }
        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }
    }
}
