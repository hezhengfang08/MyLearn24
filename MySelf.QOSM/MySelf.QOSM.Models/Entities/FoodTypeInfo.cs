using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class FoodTypeInfo
    {
        public int FtypeId { get; set; }
        public string FtypeName { get; set; } = null!;
        public string? Remark { get; set; }
        public bool IsDeleted { get; set; }
    }
}
