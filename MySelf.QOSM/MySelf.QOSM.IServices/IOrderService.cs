using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{
    public interface IOrderService
    {
        PageModel<ViewCustomerOrderInfo> GetOrderList(int customerId,int orderState,string startTime,string endTime, string orderNo,int startIndex,int pageSize);
        List<CustomerOrderInfo> GetOrderFoods(int orderId);
        bool CancelFoodOrder(int orderId);
        bool ReceiveFoodOrder(int orderId);
        bool DeliverFoodOrder(int orderId);
        bool SignFoodOrder(int orderId);
        bool DeleteFoodOrder(int orderId);
        bool IsCustHasUnSignOrder(int customerId);
    }
}
