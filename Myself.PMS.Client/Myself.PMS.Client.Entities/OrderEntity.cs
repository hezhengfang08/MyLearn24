using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Entities
{
    public class OrderEntity
    {
        public string OrderId { get; set; }
        public string OrderType { get; set; }
        public string ServiceType { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Contacts { get; set; }
        public string Phone { get; set; }
        public DateTime FinishTime { get; set; }
        public int State { get; set; }
        public bool IsUrgent { get; set; }
        public DateTime ModifyTime { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string FileName { get; set; }
        public string Title { get; set; }

        public List<OrderImageEntity> Images { get; set; }
    }
}
