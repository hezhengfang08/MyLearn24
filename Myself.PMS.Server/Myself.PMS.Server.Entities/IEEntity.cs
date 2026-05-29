using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("income_expenses")]
    public class IEEntity
    {
        [SugarColumn(ColumnName = "ie_id", IsPrimaryKey = true, IsIdentity = true)]
        public int IEID { get; set; }

        [SugarColumn(ColumnName = "happend_time")]
        public DateTime HappendTime { get; set; }

        [SugarColumn(ColumnName = "record_type")]
        public int RecordType { get; set; }

        [SugarColumn(ColumnName = "amount")]
        public decimal Amount { get; set; }

        [SugarColumn(ColumnName = "amount_type")]
        public string AmountType { get; set; }

        [SugarColumn(ColumnName = "amount_desc")]
        public string AmountDesc { get; set; }

        [SugarColumn(ColumnName = "project_name")]
        public string ProjectName { get; set; }

        [SugarColumn(ColumnName = "account")]
        public string Account { get; set; }

        [SugarColumn(ColumnName = "department")]
        public string Department { get; set; }

        [SugarColumn(ColumnName = "operator")]
        public string Operator { get; set; }

        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        [SugarColumn(ColumnName = "record_id")]
        public int? RecordId { get; set; }

        [SugarColumn(ColumnName = "record_name")]
        public string? RecordName { get; set; }

        [SugarColumn(ColumnName = "record_time")]
        public DateTime? RecordTime { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }


        [SugarColumn(ColumnName = "modity_time")]
        public DateTime ModifyTime { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }
    }
}
