using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("base_auto_color")]
    public class BaseAutoColor
    {
        [Key]
        [Column("color_id")]
        public int ColorId { get; set; }
        [Column("color_name")]
        public string? ColorName { get; set; }
    }
}
