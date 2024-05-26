using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class FoodInfo
    {
        public int FoodId { get; set; }
        public string FoodName { get; set; } = null!;
        public int FtypeId { get; set; }
        public decimal FoodPrice { get; set; }
        public string? FoodImg { get; set; }
        public decimal PackAmount { get; set; }
        public bool IsOn { get; set; }
        public string? Remark { get; set; }
        public bool IsDeleted { get; set; }
    }
}

