using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable(TableName = "base_info")]
    public class BaseInfo
    {
        [SugarColumn(ColumnName = "info_id", IsPrimaryKey = true, IsIdentity = true)]
        public int InfoId { get; set; }

        [SugarColumn(ColumnName = "info_header")]
        public string InfoHeader { get; set; }

        [SugarColumn(ColumnName = "info_content")]
        public string InfoContent { get; set; }
        [SugarColumn(ColumnName = "info_key")]
        public string InfoKey { get; set; }
        [SugarColumn(ColumnName = "info_type")]
        public int InfoType { get; set; }
        public int state { get; set; }

        [SugarColumn(ColumnName = "modify_time")]
        public DateTime ModifyTime { get; set; }
        [SugarColumn(ColumnName = "publish_time")]
        public DateTime? PublishTime { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }
        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }
    }
}

