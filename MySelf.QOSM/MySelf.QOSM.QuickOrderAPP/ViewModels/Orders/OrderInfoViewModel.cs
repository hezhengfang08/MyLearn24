using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.ViewModels.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Orders
{
   public class OrderInfoViewModel:ViewModelBase
    {
        IOrderService orderService = InstanceFactory.CreateInstance<IOrderService>();
        public OrderInfoViewModel(OrderModel orderModel)
        {
            InitOrderInfo(orderModel);//加载订单信息
            InitCommands();
        }

        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
        }

        #region 属性
        public ViewCustomerOrderInfo Order { get; set; }
        public List<OrderFoodItem> OrderFoods { get; set; }
        public string PayWayText { get; set; }
        public string OrderStateText { get; set; }
        #endregion

        //订单信息加载
        private void InitOrderInfo(OrderModel model)
        {
            Order = model.Order;
            List<CustomerOrderInfo> foods = orderService.GetOrderFoods(Order.OrderId);
            OrderFoods = new List<OrderFoodItem>();
            foods.ForEach(f => OrderFoods.Add(new OrderFoodItem()
            {
                FoodId = f.FoodId,
                FoodName = f.Food.FoodName,
                Amount = f.Amount,
                Count = f.OrderCount,
                OrderRemark = f.OrderRemark
            }));
            PayWayText = model.PayWayText;
            OrderStateText = model.OrderStateText;
        }
    }
}
