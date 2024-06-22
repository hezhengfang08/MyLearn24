using Microsoft.EntityFrameworkCore;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class OrderService : BaseService, IOrderService
    {

        public PageModel<ViewCustomerOrderInfo> GetOrderList(int custId, int orderState, string startTime, string endTime, string orderNo, int startIndex, int pageSize)
        {
            PageModel<ViewCustomerOrderInfo> result = new PageModel<ViewCustomerOrderInfo>();
            var orderList = orderState > -1 ? Query<ViewCustomerOrderInfo>(o => o.OrderState == orderState) : Query<ViewCustomerOrderInfo>();
            if (orderList.Any())
            {
                if (custId > 0)
                {
                    orderList = orderList.Where(o => o.CustomerId == custId);
                }
                if (orderList.Any() && !string.IsNullOrEmpty(startTime))
                {
                    orderList = orderList.Where(o => o.OrderTime >= DateTime.Parse(startTime));
                }
                if (orderList.Any() && !string.IsNullOrEmpty(endTime))
                {
                    orderList = orderList.Where(o => o.OrderTime <= DateTime.Parse(endTime));
                }
                if (orderList.Any() && !string.IsNullOrEmpty(orderNo))
                {
                    orderList = orderList.Where(o => o.OrderNo.Contains(orderNo));
                }
                if (orderList.Any())
                {
                    if (orderList.Count() > pageSize)
                        result.DataList = orderList.Skip(startIndex - 1).Take(pageSize).ToList();
                    else
                        result.DataList = orderList.ToList();
                    result.TotalCount = orderList.Count();
                }
            }

            return result;
        }

        public List<CustomerOrderInfo> GetOrderFoods(int orderId)
        {
            return Query<CustomerOrderInfo>(f => f.OrderId == orderId).Include(f => f.Food).ToList();
        }

        public bool CancelFoodOrder(int orderId)
        {
            return UpdateOrderState(orderId, 0);
        }


        public bool ReceiveFoodOrder(int orderId)
        {
            return UpdateOrderState(orderId, 2);
        }


        public bool DeliverFoodOrder(int orderId)
        {
            return UpdateOrderState(orderId, 3);
        }



        //修改订单状态
        private bool UpdateOrderState(int orderId, int state)
        {
            FoodOrderInfo orderInfo = Find<FoodOrderInfo>(orderId);//订单信息
            bool blUpdate = false;
            if (orderInfo != null)
            {
                orderInfo.OrderState = state;
                if (state == 3)//派送
                {
                    orderInfo.DeliveryTime = DateTime.Now;
                }
                else if (state == 4)//签收 
                {
                    orderInfo.CompleteTime = DateTime.Now;
                }
                Update(orderInfo);//更新
                if (dbContext.Entry(orderInfo).State == EntityState.Unchanged)
                {
                    blUpdate = true;
                    Detach(orderInfo);
                }
            }
            return blUpdate;
        }


        public bool DeleteFoodOrder(int orderId)
        {
            //已取消或已完成-----删除（真删除）
            FoodOrderInfo orderInfo = Find<FoodOrderInfo>(orderId);
            if (orderInfo != null)
            {
                var orderFoods = Query<CustomerOrderInfo>(of => of.OrderId == orderId).ToList();
                return ExecuteTrans(() =>
                {
                    Delete<CustomerOrderInfo>(orderFoods);
                    Delete(orderInfo);
                }, () => { });
            }
            return false;
        }



        public bool SignFoodOrder(int orderId)
        {
            return UpdateOrderState(orderId, 4);
        }

        public bool IsCustHasUnSignOrder(int customerId)
        {
            return Query<FoodOrderInfo>(o => o.CustomerId == customerId && o.OrderState > 0 && o.OrderState < 4).Any();
        }
    }
}
