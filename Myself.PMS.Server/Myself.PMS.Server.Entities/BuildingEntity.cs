using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("buildings")]
    public class BuildingEntity
    {
        [SugarColumn(ColumnName = "b_id", IsPrimaryKey = true)]
        public int Id { get; set; }
        [SugarColumn(ColumnName = "b_name")]
        public string Name { get; set; }
        [SugarColumn(ColumnName = "q_id")]
        public int Qid { get; set; }
        [SugarColumn(ColumnName = "q_name")]
        public string Qname { get; set; }
    }
}
