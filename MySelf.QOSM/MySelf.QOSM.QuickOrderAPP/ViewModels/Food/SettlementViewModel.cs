using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Food
{
   public class SettlementViewModel:ViewModelBase
    {
        IFoodService foodService = InstanceFactory.CreateInstance<IFoodService>();
        public SettlementViewModel(List<OrderFoodItem> orderFoods)
        {
            SettleFoods = orderFoods;
            TotalAmount = SettleFoods.Sum(f => f.Amount + f.PackAmounts);
            TotalPackAmounts = SettleFoods.Sum(f => f.PackAmounts);
            IsWeChat = true;
            //命令初始化
            InitCommands();
        }

        #region 属性
        public bool IsOrdered = false;//是否下单成功
        public List<OrderFoodItem> SettleFoods { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalPackAmounts { get; set; }

        int payWay;//支付的方式 1 现金  2 微信  3 支付宝

        private bool isCash;
        //是否现金支付
        public bool IsCash
        {
            get { return isCash; }
            set
            {
                isCash = value;
                SetPayWay(isCash, 1);
            }
        }

        private bool isWeChat;
        //是否微信支付
        public bool IsWeChat
        {
            get { return isWeChat; }
            set
            {
                isWeChat = value;
                SetPayWay(isWeChat, 2);
            }
        }

        private bool isAlipay;
        //是否支付宝支付
        public bool IsAlipay
        {
            get { return isAlipay; }
            set
            {
                isAlipay = value;
                SetPayWay(isAlipay, 3);
            }
        }
        #endregion

        #region 命令
        //结算提交命令
        public ICommand ConfirmCmd { get; set; }
        #endregion

        #region 方法
        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd = new RelayCommand(win =>
            {
                AddFoodOrder(win);
            });
        }

        //设置支付方式
        private void SetPayWay(bool blChoose, int val)
        {
            if (blChoose)
                payWay = val;
        }

        //点餐订单下单处理--订单信息，同时保存订单相关的菜单明细----IsOrdered--True,关闭结算窗口
        private void AddFoodOrder(object win)
        {
            if (CommonHelper.loginUser != null)//客户必须登录后，才会结算
            {
                FoodOrderInfo orderInfo = new FoodOrderInfo();
                orderInfo.CustomerId = CommonHelper.loginUser.LoginId;//客户编号
                orderInfo.TotalAmount = TotalAmount;
                orderInfo.TotalPackAmount = TotalPackAmounts;
                orderInfo.PayWay = payWay;
                orderInfo.IsPay = payWay == 1 ? false : true;
                orderInfo.OrderState = 1;// 状态 1 已下单   2 已处理  3 已派送   4 已完成
                //订单菜单列表
                List<CustomerOrderInfo> orderFoods = new List<CustomerOrderInfo>();
                SettleFoods.ForEach(f => orderFoods.Add(new CustomerOrderInfo()
                {
                    FoodId = f.FoodId,
                    OrderCount = f.Count,
                    Amount = f.Amount + f.PackAmounts,
                    OrderRemark = f.OrderRemark
                }));
                bool blAdd = foodService.AddFoodOrder(orderInfo, orderFoods);
                if (blAdd)
                {
                    //下单成功
                    ShowSuccess("你的点餐已成功下单！", "点餐结算");
                    IsOrdered = true;//已下单
                    CloseWindow(win);//关闭结算页
                }
                else
                {
                    ShowError("你的点餐下单失败！", "点餐结算");
                    return;
                }
            }
            else
            {
                ShowError("请先登录快捷点餐系统后才能结算！", "点餐结算");
                return;
            }
        }
        #endregion
    }
}
