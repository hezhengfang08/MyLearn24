using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using sw = System.Windows;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class OrderViewModel : ViewModelBase<OrderModel>
    {
        // 条件检索对象
        public OrderInfo SearchModel { get; set; } =
            new OrderInfo();

        // 订单列表
        public ObservableCollection<OrderModel> OrderList { get; set; } =
            new ObservableCollection<OrderModel>();


        IRecordService _recordService;
        public OrderViewModel(
            IRegionManager regionManager,
            IRecordService recordService) : base(regionManager)
        {
            this.PageTitle = "订单信息管理";

            _recordService = recordService;

            this.Refresh();
        }

        public override void Refresh()
        {
            OrderList.Clear();

            //// 动态表达式，表达式目录树（高级班）
            //ParameterExpression expression = Expression.Parameter(typeof(OrderInfo), "oi");
            //// if(SearchModel.OrderId)
            //// 添加OrderId条件
            //MemberExpression parameter = Expression.Property(expression, "OrderId");
            //// 创建对应的条件值：常量表达式
            //ConstantExpression constant = Expression.Constant(27);
            //// 创建条件关系
            //BinaryExpression comparision = Expression.Equal(parameter, constant);

            //// if(SearchModel.License)
            //parameter = Expression.Property(expression, "AutoLicense");
            //constant = Expression.Constant("123");
            //MethodCallExpression mce = Expression.Call(parameter, typeof(string).GetMethod("Contains", new [] { typeof(string) }), constant);

            //// 

            /////

            ////


            ////需要两个参数，两个参数这间使用&&连接符
            //BinaryExpression lambdaExpression = Expression.AndAlso(comparision, mce);

            //var lambda = Expression.Lambda<Func<OrderInfo, bool>>(lambdaExpression, expression);
            ////q => q.OrderId == 27 && q.AutoLicense.Contains("123")

            //var temp = _recordService.Query<OrderInfo>(CreateLambda());




            var orders = _recordService.Query<OrderInfo>(CreateLambda());
            foreach (var order in orders)
            {
                OrderModel model = new OrderModel();
                OrderList.Add(model);

                model.OrderId = (int)order.OrderId;
                model.AutoLicense = order.AutoLicense;

                model.EnterTime = order.EnterTime;
                model.LeaveTime = order.LeaveTime;
                //model.FeeModeId = (int)order.FeeModeId;// 应该显示的是名称
                model.State = (int)order.State;
                model.Payable = order.Payable == null ? 0 : (long)order.Payable;
                model.Payment = order.Payment == null ? 0 : (long)order.Payment;
                model.Discount = order.Discount == null ? 0 : (long)order.Discount;

                // 计费模式名称
                var fee = _recordService.Find<BaseFeeMode>((int)order.FeeModeId);
                model.FeeMode = fee.FeeModeName;

                // 入口记录信息
                model.EnterRecord = (int)order.EnterRecord;
                var enter = _recordService.Find<RecordInfo>(model.EnterRecord);
                model.EnterPassTime = enter.PassTime;
                model.EnterChannal = enter.Channal;
                model.EnterImageFull = "pack://siteoforigin:,,,/snaps/" + enter.ImageFull;
                model.EnterImageSmall = "pack://siteoforigin:,,,/snaps/" + enter.ImageSmall;
                var car = _recordService.Find<BaseAutoColor>((int)enter.CarColor);
                model.EnterCarColor = car.ColorName;
                var lic = _recordService.Find<BaseLicenseColor>((int)enter.LicenseColor);
                model.EnterLicenseColor = lic.ColorName;

                // 出口记录信息
                if (order.LeaveRecord == null) continue;
                model.LeaveRecord = (int)order.LeaveRecord;
                var exit = _recordService.Find<RecordInfo>(model.LeaveRecord);
                if (exit == null) continue;
                model.LeavePassTime = exit.PassTime;
                model.LeaveChannal = exit.Channal;
                model.LeaveImageFull = "pack://siteoforigin:,,,/snaps/" + exit.ImageFull;
                model.LeaveImageSmall = "pack://siteoforigin:,,,/snaps/" + exit.ImageSmall;

                car = _recordService.Find<BaseAutoColor>((int)enter.CarColor);
                model.LeaveCarColor = car.ColorName;
                lic = _recordService.Find<BaseLicenseColor>((int)enter.LicenseColor);
                model.LeaveLicenseColor = lic.ColorName;
            }


        }

        private Expression<Func<OrderInfo, bool>> CreateLambda()
        {
            PropertyInfo[] pis = SearchModel.GetType().GetProperties();

            // "select * from order where 1=1 ";
            // and order_id='123'
            // and license='12312'";

            ParameterExpression expression = Expression.Parameter(typeof(OrderInfo), "oi");
            // 默认1==1条件，用来拼接后续条件
            ConstantExpression constant1 = Expression.Constant(1);
            Expression combinedExpression = Expression.Equal(constant1, constant1);
            foreach (var pi in pis)
            {
                // 添加OrderId条件
                MemberExpression parameter = Expression.Property(expression, pi);
                // 创建对应的条件值：常量表达式
                var condition_value = pi.GetValue(SearchModel);
                if (condition_value == null) continue;
                if (pi.Name == "OrderId" && condition_value.ToString() == "0") continue;
                // 对比值
                ConstantExpression constant = Expression.Constant(condition_value, pi.PropertyType);
                // 创建条件关系
                if (pi.PropertyType == typeof(string))
                {
                    // containst
                    MethodCallExpression mce = Expression.Call(parameter,
                        typeof(string).GetMethod("Contains", new[] { typeof(string) }), constant);
                    combinedExpression = Expression.AndAlso(combinedExpression, mce);
                }
                else
                {
                    BinaryExpression comparision = Expression.Equal(parameter, constant);
                    combinedExpression = Expression.AndAlso(combinedExpression, comparision);
                }
            }

            var lambda = Expression.Lambda<Func<OrderInfo, bool>>(combinedExpression, expression);

            return lambda;
        }

        public override void DoModify(OrderModel model)
        {
            // 这个动作不可以逆
            // 需要用户做确认
            try
            {
                if (sw.MessageBox.Show("是否确定作废此订单？", "提示", sw.MessageBoxButton.YesNo) ==
                    sw.MessageBoxResult.Yes)
                {
                    var order = _recordService.Find<OrderInfo>((model ).OrderId);
                    order.State = -1;
                    _recordService.Update(order);

                    sw.MessageBox.Show("订单已作废！", "提示");

                    //Users.Remove(model as UserModelEx);
                    (model).State = -1;
                }
            }
            catch (Exception ex)
            {
                sw.MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
