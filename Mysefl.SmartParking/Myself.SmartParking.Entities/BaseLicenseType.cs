using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("base_license_type")]
    public class BaseLicenseType
    {
        [Key]
        [Column("type_id")]
        public int TypeId { get; set; }
        [Column("type_name")]
        public string? TypeName { get; set; }
    }
}
