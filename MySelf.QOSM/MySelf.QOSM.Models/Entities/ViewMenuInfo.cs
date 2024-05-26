using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class ViewMenuInfo
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; } = null!;
        public int ParentId { get; set; }
        public string? ParentName { get; set; }
        public string? MenuPic { get; set; }
        public string? MenuUrl { get; set; }
        public int MenuOrder { get; set; }
        public bool IsSysMenu { get; set; }
        public bool IsDeleted { get; set; }
    }
}
