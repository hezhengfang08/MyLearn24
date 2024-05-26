using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class RoleMenuInfo
    {
        public int Rmid { get; set; }
        public int RoleId { get; set; }
        public int MenuId { get; set; }
        public bool IsDeleted { get; set; }
    }
}

