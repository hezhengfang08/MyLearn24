using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("base_fee_mode")]
    public class BaseFeeMode
    {
        [Key]
        [Column("fee_mode_id")]
        public int FeeModeId { get; set; }
        [Column("fee_mode_name")]
        public string? FeeModeName { get; set; }
    }
}
