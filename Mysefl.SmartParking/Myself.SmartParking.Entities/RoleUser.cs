using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Entities
{
    [Table("role_user")]
    public class RoleUser
    {
        [Column("role_id")]
        public int RoleId { get; set; }
        [Column("user_id")]
        public int UserId { get; set; }

        [NotMapped]
        public SysRole SysRole { get; set; }
    }
}
