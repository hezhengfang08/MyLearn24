using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Orders
{
   public class OrderModel:ModelBase
    {
        bool isUser = CommonHelper.loginUser.IsUser;//是否后台用户
        public OrderModel(ViewCustomerOrderInfo info)
        {
            Order = info;
            ShowInfo = Visibility.Visible;
            //后台用户才能接单
            ShowGet = (info.OrderState == 1 && isUser) ? Visibility.Visible : Visibility.Collapsed;
            //未派送前可以退单
            ShowCancel = ((info.OrderState == 1 || info.OrderState == 2) && !isUser) ? Visibility.Visible : Visibility.Collapsed;
            ShowDeliver = (info.OrderState == 2 && isUser) ? Visibility.Visible : Visibility.Collapsed;
            ShowSignFor = (info.OrderState == 3 && !isUser) ? Visibility.Visible : Visibility.Collapsed;
            ShowDelete = ((info.OrderState == 4 || info.OrderState == 0) && !isUser) ? Visibility.Visible : Visibility.Collapsed;
        }

        //Order---ViewCustomerOrderInfo
        private ViewCustomerOrderInfo order;
        //订单信息
        public ViewCustomerOrderInfo Order
        {
            get { return order; }
            set
            {
                order = value;
                OnPropertyChanged();
            }
        }

        //支付方式描述
        public string PayWayText
        {
            get
            {
                string strPayWay = "";
                switch (order.PayWay)
                {
                    case 1: strPayWay = "现金"; break;
                    case 2: strPayWay = "微信"; break;
                    case 3: strPayWay = "支付宝"; break;
                }
                return strPayWay;
            }
        }

        private string stateText;
        //订单状态描述
        public string OrderStateText
        {
            get
            {
                stateText = "";
                switch (order.OrderState)
                {
                    case 0: stateText = "已退单"; break;
                    case 1: stateText = "已下单"; break;
                    case 2: stateText = "已接单"; break;
                    case 3: stateText = "已派送"; break;
                    case 4: stateText = "已完成"; break;
                }
                return stateText;
            }
            set
            {
                stateText = value;
                OnPropertyChanged();
            }
        }


        private Visibility showInfo;
        //显示详情
        public Visibility ShowInfo
        {
            get { return showInfo; }
            set
            {
                showInfo = value;
                OnPropertyChanged();
            }
        }

        private Visibility showGet;
        //显示接单
        public Visibility ShowGet
        {
            get { return showGet; }
            set
            {
                showGet = value;
                OnPropertyChanged();
            }
        }

        private Visibility showCancel;
        //显示退单
        public Visibility ShowCancel
        {
            get { return showCancel; }
            set
            {
                showCancel = value;
                OnPropertyChanged();
            }
        }

        private Visibility showDeliver;
        //显示派送
        public Visibility ShowDeliver
        {
            get { return showDeliver; }
            set
            {
                showDeliver = value;
                OnPropertyChanged();
            }
        }

        private Visibility showSignFor;
        //显示签收
        public Visibility ShowSignFor
        {
            get { return showSignFor; }
            set
            {
                showSignFor = value;
                OnPropertyChanged();
            }
        }

        private Visibility showDelete;
        //显示删除
        public Visibility ShowDelete
        {
            get { return showDelete; }
            set
            {
                showDelete = value;
                OnPropertyChanged();
            }
        }
    }
}
