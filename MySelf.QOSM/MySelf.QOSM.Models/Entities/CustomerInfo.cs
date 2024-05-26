using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class CustomerInfo
    {
        public int CustomerId { get; set; }
        public string CustomerNo { get; set; } = null!;
        public string CustomerName { get; set; } = null!;
        public string CustomerPwd { get; set; } = null!;
        public string Sex { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string Address { get; set; } = null!;
        public bool? CustomerState { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime? LogOffTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}

