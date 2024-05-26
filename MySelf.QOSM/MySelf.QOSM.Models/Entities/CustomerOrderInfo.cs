using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class CustomerOrderInfo
    {
        public int Rfid { get; set; }
        public int OrderId { get; set; }
        public int FoodId { get; set; }
        public int OrderCount { get; set; }
        public decimal Amount { get; set; }
        public string? OrderRemark { get; set; }
    }
}

