using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("contracts")]
    public class ContractEntity
    {
        [SugarColumn(ColumnName = "cid", IsPrimaryKey = true, IsIdentity = true)]
        public int ContractId { get; set; }

        [SugarColumn(ColumnName = "c_name")]
        public string? ContractName { get; set; }

        [SugarColumn(ColumnName = "c_num")]
        public string? ContractNumber { get; set; }

        [SugarColumn(ColumnName = "c_amount")]
        public decimal? ContractAmount { get; set; }

        [SugarColumn(ColumnName = "e_amount")]
        public decimal? ExcuteAmount { get; set; }

        [SugarColumn(ColumnName = "sign_time")]
        public DateTime? SignTime { get; set; }

        [SugarColumn(ColumnName = "operator")]
        public string? Operator { get; set; }

        [SugarColumn(ColumnName = "opposite")]
        public string? Opposite { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }

        [SugarColumn(ColumnName = "archived_time")]
        public DateTime? ArchivedTime { get; set; }

        [SugarColumn(ColumnName = "archived_user_id")]
        public int? ArchivedUserId { get; set; }

        [SugarColumn(ColumnName = "archived_user_name")]
        public string? ArchivedUserName { get; set; }

        [SugarColumn(ColumnName = "modify_time")]
        public DateTime? ModifyTime { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int? Userid { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string? UserName { get; set; }
    }
}
