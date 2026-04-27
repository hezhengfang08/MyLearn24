using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("member_recharge")]
    public class MemberRecharge
    {
        [Key]
        [Column("id")]
        public int RId { get; set; }

        [Column("auto_lisence")]
        public string AutoLisence { get; set; }

        [Column("fee_mode_id")]
        public int FeeModeId { get; set; }
        [NotMapped]
        public string FeeModeName { get; set; }

        [Column("recharge_count")]
        public int? RechargeCount { get; set; }

        [Column("recharge_start_time")]
        public string? RechargeStartTime { get; set; }

        [Column("recharge_end_time")]
        public string? RechargeEndTime { get; set; }

        [Column("state")]
        public int State { get; set; }

        [Column("create_time")]
        public string CreateTime { get; set; }

        [Column("create_id")]
        public int CreateId { get; set; }

        [Column("create_name")]
        public string CreateName { get; set; }
    }
}
