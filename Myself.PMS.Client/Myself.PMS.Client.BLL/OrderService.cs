using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class OrderService : BaseService, IOrderService
    {
        IOrderAccess _orderAccess;
        public OrderService(IOrderAccess orderAccess)
        {
            _orderAccess = orderAccess;
        }
        public PageEntity<OrderEntity[]> GetOrderPage(string key, int index, int size)
        {
            string json = _orderAccess.GetOrders(key, index, size);
            return this.GetResult<PageEntity<OrderEntity[]>>(json);
        }
    }
}
