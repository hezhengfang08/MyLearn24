using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("quarters")]
    public class QuarterEntity
    {
        [SugarColumn(ColumnName = "q_id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnName = "q_name")]
        public string Name { get; set; }
    }
}
