using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("record")]
    public class RecordInfo
    {
        [Key]
        [Column("record_id")]
        public int RecordId { get; set; }
        [Column("auto_license")]
        public string? AutoLicense { get; set; }
        [Column("pass_time")]
        public string? PassTime { get; set; }
        [Column("channal")]
        public int? Channal { get; set; }
        [Column("car_color")]
        public int? CarColor { get; set; }
        [Column("license_color")]
        public int? LicenseColor { get; set; }


        [Column("img_full")]
        public string? ImageFull { get; set; }
        [Column("img_small")]
        public string? ImageSmall { get; set; }
    }
}
