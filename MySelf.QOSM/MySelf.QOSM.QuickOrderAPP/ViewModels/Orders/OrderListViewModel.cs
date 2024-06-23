using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using MySelf.QOSM.QuickOrderAPP.ViewModels.UControl;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MySelf.QOSM.BLLFactory;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Orders
{
    public class OrderListViewModel : ViewModelBase
    {
        IOrderService orderService = InstanceFactory.CreateInstance<IOrderService>();
        int oldState = -1;
        int custId = 0;
        public OrderListViewModel()
        {
            if (!CommonHelper.loginUser.IsUser)
                custId = CommonHelper.loginUser.LoginId;
            LoadCboOrderStates();//加载订单状态下拉框
            OrderStartTime = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");//下单开始时间
            OrderEndTime = "";
            PageInfoVM = new UPagerViewModel();
            PageInfoVM.PageSize = 15;
            PageInfoVM.PageChanged += (o, e) => LoadOrderList();//调用订单查询
            LoadOrderList();//加载昨天以来的订单列表
            InitCommands();//命令初始化
        }

        private void InitCommands()
        {
            FindOrderListCmd = new RelayCommand(o =>
            {
                LoadOrderList();
            });
            InfoCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                ShowOrderInfoWindow(order);
            });
            GetOrderCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                ReceiveOrder(order);
            });

            CancelOrderCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                CancelOrder(order);
            });

            DeliverCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                DeliverFood(order);
            });

            SignForCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                SignForOrder(order);
            });

            DelOrderCmd = new RelayCommand(o =>
            {
                OrderModel order = o as OrderModel;
                DeleteOrder(order);
            });
        }
        #region 属性
        //订单状态列表
        public List<OrderStateItem> OrderStates { get; set; }
        //选择的订单状态值
        public int OrderState { get; set; }
        //下单开始时间
        public string OrderStartTime { get; set; }
        //下单结束时间
        public string OrderEndTime { get; set; }
        //订单号关键词
        public string OrderNo { get; set; }

        private ObservableCollection<OrderModel> orderList;
        //订单列表
        public ObservableCollection<OrderModel> OrderList
        {
            get { return orderList; }
            set
            {
                orderList = value;
                OnPropertyChanged();
            }
        }

        private UPagerViewModel pageInfoVM;
        //分页控件DataContext
        public UPagerViewModel PageInfoVM
        {
            get { return pageInfoVM; }
            set
            {
                pageInfoVM = value;
            }
        }
        #endregion

        #region 命令
        //查询订单命令
        public ICommand FindOrderListCmd { get; set; }
        //详情命令
        public ICommand InfoCmd { get; set; }
        //接单命令
        public ICommand GetOrderCmd { get; set; }
        //退单命令
        public ICommand CancelOrderCmd { get; set; }
        //派送命令
        public ICommand DeliverCmd { get; set; }
        //签收命令
        public ICommand SignForCmd { get; set; }
        //删除订单命令
        public ICommand DelOrderCmd { get; set; }
        #endregion

        #region 方法
        //加载订单状态列表
        private void LoadCboOrderStates()
        {
            OrderStates = new List<OrderStateItem>()
             {
                 new OrderStateItem(-1,"全部"),
                 new OrderStateItem(1,"已下单"),
                 new OrderStateItem(2,"已接单"),
                  new OrderStateItem(3,"已派送"),
                 new OrderStateItem(4,"已完成"),
                 new OrderStateItem(0,"已退单")
             };
            OrderState = -1;
        }

        //条件查询订单列表
        private void LoadOrderList()
        {
            if (OrderState != oldState)
            {
                PageInfoVM.CurrentPage = 1;
                oldState = OrderState;
            }
            OrderList = new ObservableCollection<OrderModel>();
            //调用查询方法
            PageModel<ViewCustomerOrderInfo> result = orderService.GetOrderList(custId, OrderState, OrderStartTime, OrderEndTime, OrderNo, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if (result != null)
            {
                List<ViewCustomerOrderInfo> orderData = null;
                if (result.TotalCount > 0)
                {
                    orderData = result.DataList;//订单列表
                    PageInfoVM.TotalCount = result.TotalCount;//总记录数
                    orderData.ForEach(o => OrderList.Add(new OrderModel(o)));
                }
                else
                {
                    PageInfoVM.TotalCount = 0;

                }
            }

        }

        //显示订单详情窗口
        private void ShowOrderInfoWindow(OrderModel order)
        {
            OrderInfoViewModel infoVM = new OrderInfoViewModel(order);
            ShowDialog("orderInfoWindow", infoVM);
        }

        //接单处理
        private void ReceiveOrder(OrderModel order)
        {
            if (order != null)
            {
                if (ShowQuestion("你确定要接收处理该订单吗？", "接单提示") == MessageBoxResult.OK)
                {
                    ViewCustomerOrderInfo orderInfo = order.Order;
                    bool blReceive = orderService.ReceiveFoodOrder(orderInfo.OrderId);
                    if (blReceive)
                    {
                        ShowSuccess("该订单已接收处理！", "接单提示");
                        orderInfo.OrderState = 2;//已接单
                        order.OrderStateText = "已接单";
                        order.Order = orderInfo;
                        order.ShowGet = Visibility.Collapsed;
                        order.ShowDeliver = Visibility.Visible;
                    }
                }
            }
        }

        //退单处理
        private void CancelOrder(OrderModel order)
        {
            if (order != null)
            {
                if (ShowQuestion("你确定要取消该订单吗？", "退单提示") == MessageBoxResult.OK)
                {
                    ViewCustomerOrderInfo orderInfo = order.Order;
                    bool blCancel = orderService.CancelFoodOrder(orderInfo.OrderId);
                    if (blCancel)
                    {
                        ShowSuccess("该订单已退单成功！", "退单提示");
                        orderInfo.OrderState = 0;//已退单
                        order.OrderStateText = "已退单";
                        order.Order = orderInfo;
                        order.ShowCancel = Visibility.Collapsed;
                        order.ShowDelete = Visibility.Visible;//可删除
                    }
                }
            }
        }

        //派送处理
        private void DeliverFood(OrderModel order)
        {
            if (order != null)
            {
                if (ShowQuestion("你确定要安排该订单派送吗？", "派送提示") == MessageBoxResult.OK)
                {
                    ViewCustomerOrderInfo orderInfo = order.Order;
                    bool blDeliver = orderService.DeliverFoodOrder(orderInfo.OrderId);
                    if (blDeliver)
                    {
                        ShowSuccess("该订单安排派送成功！", "派送提示");
                        orderInfo.OrderState = 3;//已派送
                        order.OrderStateText = "已派送";
                        order.Order = orderInfo;
                        order.ShowDeliver = Visibility.Collapsed;

                    }
                }
            }
        }

        //签收处理---显示签收窗口，----更新订单状态
        private void SignForOrder(OrderModel order)
        {
            if (order != null)
            {
                SignForFoodViewModel signForVM = new SignForFoodViewModel(order);
                ShowDialog("signForWindow", signForVM);//显示签收页
                if (signForVM.isSignFor)
                {
                    ViewCustomerOrderInfo orderInfo = order.Order;
                    orderInfo.OrderState = 4;//已完成
                    order.OrderStateText = "已完成";
                    order.Order = orderInfo;
                    order.ShowSignFor = Visibility.Collapsed;
                    order.ShowDelete = Visibility.Visible;
                }
            }
        }

        //删除订单处理---已退单或已完成
        private void DeleteOrder(OrderModel order)
        {
            if (order != null)
            {
                if (ShowQuestion("你确定要删除该订单吗？", "删除提示") == MessageBoxResult.OK)
                {
                    ViewCustomerOrderInfo orderInfo = order.Order;
                    bool blDelete = orderService.DeleteFoodOrder(orderInfo.OrderId);
                    if (blDelete)
                    {
                        ShowSuccess("该订单删除成功！", "删除提示");
                        OrderList.Remove(order);
                    }
                }
            }
        }
        #endregion
    }
}

