using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Service
{
    public class OrderService : IOrderService
    {
        ISqlSugarClient _client;
        public OrderService(ISqlSugarClient client)
        {
            _client = client;
        }
        public OrderEntity[] GetOrders(string key, int index, int size, ref int totalCount)
        {
            return _client.Queryable<OrderEntity>()
                .Where(e =>
                        string.IsNullOrEmpty(key) ||
                        e.OrderId.Contains(key) ||
                        e.Description.Contains(key) ||
                        e.Address.Contains(key) ||
                        e.Contacts.Contains(key) ||
                        e.Phone.Contains(key)
                        )
                .Select(e => new OrderEntity()
                {
                    Images = SqlFunc.Subqueryable<OrderImageEntity>()
                    .Where(ru => ru.OrderId == e.OrderId).ToList()
                })
                .ToPageList(index, size, ref totalCount)
                .ToArray();
        }
    }
}
