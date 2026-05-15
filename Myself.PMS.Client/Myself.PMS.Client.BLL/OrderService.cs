using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
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

        public int ChangeState(string id, int state)
        {
            string json = _orderAccess.ChangeState(id, state);
            return this.GetResult<int>(json);
        }

        public int DeleteOrder(string id)
        {
            string json = _orderAccess.DeleteOrder(id);
            return this.GetResult<int>(json);
        }

        public PageEntity<OrderEntity[]> GetOrderPage(string key, int index, int size)
        {
            string json = _orderAccess.GetOrders(key, index, size);
            return this.GetResult<PageEntity<OrderEntity[]>>(json);
        }

        public int UpdateOrder(OrderEntity entity)
        {
            string json = "";
            try
            {
                json = entity.Serialize();
                json = _orderAccess.UpdateOrder(json);
                return this.GetResult<int>(json);
            }
            catch
            {
                throw new Exception("接口请求出错！" + json);
            }
            ;
        }
    }
}
