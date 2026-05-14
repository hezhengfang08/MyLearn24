using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Entities
{
    [SugarTable("fee_info")]
    public class FeeEntity
    {
        [SugarColumn(ColumnName = "fee_id", IsPrimaryKey = true, IsIdentity = true)]
        public int FeeId { get; set; }

        [SugarColumn(ColumnName = "fee_mode_id")]
        public int FeeModeId { get; set; }

        [SugarColumn(ColumnName = "fee_mode")]
        public string FeeMode { get; set; }

        [SugarColumn(ColumnName = "amount")]
        public decimal Amount { get; set; }


        [SugarColumn(ColumnName = "b_id")]
        public int BId { get; set; }

        [SugarColumn(ColumnName = "b_name")]
        public string BName { get; set; }

        [SugarColumn(ColumnName = "q_id")]
        public int QId { get; set; }

        [SugarColumn(ColumnName = "q_name")]
        public string QName { get; set; }

        [SugarColumn(ColumnName = "room_num")]
        public string RoomNumber { get; set; }

        [SugarColumn(ColumnName = "description")]
        public string Description { get; set; }

        [SugarColumn(ColumnName = "state")]
        public int State { get; set; }

        [SugarColumn(ColumnName = "user_id")]
        public int UserId { get; set; }

        [SugarColumn(ColumnName = "user_name")]
        public string UserName { get; set; }

        [SugarColumn(ColumnName = "modify_time")]
        public DateTime ModifyTime { get; set; }

        [SugarColumn(ColumnName = "valid_time")]
        public DateTime ValidTime { get; set; }
    }
}
