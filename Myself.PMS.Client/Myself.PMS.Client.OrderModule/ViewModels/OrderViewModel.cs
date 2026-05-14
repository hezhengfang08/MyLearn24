using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.OrderModule.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace Myself.PMS.Client.OrderModule.ViewModels
{
    public class OrderViewModel : PageViewModelBase
    {
        public ObservableCollection<OrderModel> OrderList { get; set; } =
            new ObservableCollection<OrderModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        IOrderService _orderService;
        public OrderViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IOrderService orderService) : base(regionManager, eventAggregator)
        {
            this.PageTitle = "报修管理";
            _orderService = orderService;

            BindingOperations.EnableCollectionSynchronization(OrderList, this);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            this.Refresh();
        }

        public override void Refresh()
        {
            OrderList.Clear();
            this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    var page = _orderService.GetOrderPage(this.SearchKey, PaginationModel.PageIndex, PaginationModel.PageSize);

                    int index = 0;
                    foreach (var order in page.Data)
                    {
                        index++;
                        OrderList.Add(new OrderModel
                        {
                            Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize,
                            OrderId = order.OrderId,
                            OrderType = order.OrderType,
                            Description = order.Description,
                            Address = order.Address,
                            Contacts = order.Contacts,
                            Phone = order.Phone,
                            FinishTime = order.FinishTime,
                            State = order.State,
                            IsUrgent = order.IsUrgent,
                            ModifyTime = order.ModifyTime,
                            UserId = order.UserId,
                            UserName = order.UserName,

                            ImageList = order.Images.Select(i => new OrderImageModel
                            {
                                OrderId = i.OrderId,
                                ImageId = i.ImageId,
                                ImageName = "http://localhost:5273/api/file/order_img/" + i.ImageName
                            }).ToList()
                        });
                    }

                    Application.Current.Dispatcher?.Invoke(() =>
                    {
                        // 刷新分页组件的页码
                        PaginationModel.FillPageNumbers(page.TotalCount);
                    });
                }
                catch (Exception ex)
                {

                    throw;
                }
                finally
                {
                    this.EndLoading();
                }
            });
        }

        public override void DoDelete(object model)
        {
            base.DoDelete(model);
        }

        public override void DoModify(object model)
        {
            base.DoModify(model);
        }
    }
}
