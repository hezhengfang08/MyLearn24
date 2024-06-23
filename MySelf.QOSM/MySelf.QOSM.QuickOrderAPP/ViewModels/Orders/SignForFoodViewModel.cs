using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using MySelf.QOSM.QuickOrderAPP.ViewModels.Food;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MySelf.QOSM.BLLFactory;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Orders
{
    public class SignForFoodViewModel : ViewModelBase
    {
        IOrderService orderService = InstanceFactory.CreateInstance<IOrderService>();
        public SignForFoodViewModel(OrderModel orderModel)
        {
            InitOrderInfo(orderModel);//订单加载
            InitCommands();//命令初始化
        }

        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            SignForFoodCmd = new RelayCommand(win =>
            {
                SignForFood(win);//签收过程处理
            });
        }

        #region 属性
        public ViewCustomerOrderInfo Order { get; set; }
        public List<OrderFoodItem> OrderFoods { get; set; }
        public string PayWayText { get; set; }
        public string OrderStateText { get; set; }
        #endregion
        public bool isSignFor = false;//是否签收成功

        #region 命令
        //签收命令
        public ICommand SignForFoodCmd { get; set; }
        #endregion

        #region 方法
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
            OrderStateText = "待取餐";
        }

        //签收过程
        private void SignForFood(object win)
        {
            int orderId = Order.OrderId;//订单编号
            if (ShowQuestion("你确定要签收该订单吗？", "签收提示") == MessageBoxResult.OK)
            {
                bool blSignFor = orderService.SignFoodOrder(orderId);
                if (blSignFor)
                {
                    ShowSuccess("该订单签收成功！", "签收提示");
                    isSignFor = true;//签收成功
                    CloseWindow(win);
                }
            }
        }
        #endregion
    }
}
