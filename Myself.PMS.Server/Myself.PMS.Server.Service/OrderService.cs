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

        public int ChangeState(string id, int state)
        {
            return _client.Updateable<OrderEntity>()
               .SetColumns(fe => new OrderEntity { State = state })
               .Where(oi => oi.OrderId == id)
               .ExecuteCommand();
        }

        public int DeleteOrder(string id)
        {
            int count = 0;
            _client.Ado.BeginTran();
            try
            {
                count = _client.Deleteable<OrderEntity>().In(id).ExecuteCommand();

                _client.Deleteable<OrderImageEntity>()
                    .Where(oi => oi.OrderId == id)
                    .ExecuteCommand();

                _client.Ado.CommitTran();
            }
            catch (Exception ex)
            {
                _client.Ado.RollbackTran();
                throw ex;
            }

            return count;
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

        public int UpdateOrder(OrderEntity order)
        {
            _client.Ado.BeginTran();
            try
            {
                if (string.IsNullOrEmpty(order.OrderId))
                {
                    // 新增
                    order.OrderId = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    order.Title = "需求标题";

                    foreach (var item in order.Images)
                    {
                        item.OrderId = order.OrderId;
                    }
                    _client.Insertable(order).ExecuteCommand();
                    _client.Insertable(order.Images).ExecuteCommand();
                }
                else
                {
                    _client.Updateable(order).ExecuteCommand();

                    _client.Deleteable<OrderImageEntity>()
                        .Where(oi => oi.OrderId == order.OrderId)
                        .ExecuteCommand();

                    foreach (var img in order.Images)
                    {
                        img.OrderId = order.OrderId;
                    }
                    _client.Insertable(order.Images).ExecuteCommand();
                }

                _client.Ado.CommitTran();

                return 1;
            }
            catch (Exception ex)
            {
                _client.Ado.RollbackTran();
                throw ex;
            }
        }
    }
}
