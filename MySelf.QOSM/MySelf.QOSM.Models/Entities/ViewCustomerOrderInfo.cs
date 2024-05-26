using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Models.Entities
{
    public partial class ViewCustomerOrderInfo
    {
        public int OrderId { get; set; }
        public string OrderNo { get; set; } = null!;
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = null!;
        public decimal TotalAmount { get; set; }
        public decimal TotalPackAmount { get; set; }
        public int PayWay { get; set; }
        public int OrderState { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime? DeliveryTime { get; set; }
        public DateTime? CompleteTime { get; set; }
        public string PickCode { get; set; } = null!;
    }
}

