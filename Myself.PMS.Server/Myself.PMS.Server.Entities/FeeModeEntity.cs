using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("fee_mode")]
    public class FeeModeEntity
    {
        [SugarColumn(ColumnName = "f_id", IsPrimaryKey = true)]
        public int FeeModeId { get; set; }
        [SugarColumn(ColumnName = "f_name")]
        public string FeeModeName { get; set; }
    }
}
