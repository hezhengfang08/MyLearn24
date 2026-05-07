using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("order")]
    public class OrderInfo
    {
        [Key]
        [Column("order_id")]
        public int? OrderId { get; set; }
        [Column("auto_license")]
        public string? AutoLicense { get; set; }

        [Column("enter_time")]
        public string? EnterTime { get; set; }
        [Column("leave_time")]
        public string? LeaveTime { get; set; }
        [Column("fee_mode_id")]
        public int? FeeModeId { get; set; }
        [Column("state")]
        public int? State { get; set; }
        [Column("payable")]
        public double? Payable { get; set; }// 应付
        [Column("payment")]
        public double? Payment { get; set; }
        [Column("discount")]
        public double? Discount { get; set; }
        [Column("enter_record")]
        public int? EnterRecord { get; set; }
        [Column("exit_record")]
        public int? LeaveRecord { get; set; }

    }
}
