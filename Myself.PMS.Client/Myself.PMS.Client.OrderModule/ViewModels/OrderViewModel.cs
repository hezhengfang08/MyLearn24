using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.OrderModule.Models;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Myself.PMS.Client.OrderModule.ViewModels
{
    public class OrderViewModel : PageViewModelBase
    {
        public ObservableCollection<OrderModel> OrderList { get; set; } =
            new ObservableCollection<OrderModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        public DelegateCommand<OrderModel> CancelCommand { get; set; }
        public DelegateCommand<OrderModel> RevokeCommand { get; set; }
        public DelegateCommand<OrderModel> PublishCommand { get; set; }
        public DelegateCommand<OrderModel> FinishCommand { get; set; }

        public DelegateCommand<object> ImageCommand { get; set; }

        IDialogService _dialogService;
        IOrderService _orderService;
        public OrderViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IOrderService orderService, IDialogService dialogService) : base(regionManager, eventAggregator)
        {
            this.PageTitle = "报修管理";
            BindingOperations.EnableCollectionSynchronization(OrderList, this);
            _orderService = orderService;
            _dialogService = dialogService; 

            BindingOperations.EnableCollectionSynchronization(OrderList, this);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });
            CancelCommand = new DelegateCommand<OrderModel>(DoCancel);
            RevokeCommand = new DelegateCommand<OrderModel>(DoRevoke);
            PublishCommand = new DelegateCommand<OrderModel>(DoPublish);
            FinishCommand = new DelegateCommand<OrderModel>(DoFinish);
            ImageCommand = new DelegateCommand<object>(ShowImage);
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
                            ServiceType= order.ServiceType,
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
                            Title = order.Title,

                            ImageList = new ObservableCollection<OrderImageModel>(order.Images.Select(i => new OrderImageModel
                            {
                                OrderId = i.OrderId,
                                ImageId = i.ImageId,
                                ImageName = "https://localhost:7299/api/file/order_img/" + i.ImageName
                            }).ToList())
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
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    _orderService.DeleteOrder((model as OrderModel).OrderId);

                    MessageBox.Show("删除完成！", "提示");

                    OrderList.Remove(model as OrderModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        public override void DoModify(object model)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("model", model);
            _dialogService.ShowDialog("ModifyOrderView", dps, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }

        private void DoCancel(OrderModel model)
        {
            // 状态写-1
            ChangeState(model.OrderId, -1, "作废");
            model.State = -1;
        }

        private void DoRevoke(OrderModel model)
        {
            // 状态写0
            ChangeState(model.OrderId, 0, "撤销");
            model.State = 0;
        }

        private void DoPublish(OrderModel model)
        {
            // 状态写1
            ChangeState(model.OrderId, 1, "发布");
            model.State = 1;
        }

        private void DoFinish(OrderModel model)
        {
            // 状态写2
            ChangeState(model.OrderId, 2, "完结");
            model.State = 2;
        }

        private void ChangeState(string id, int state, string tip)
        {
            try
            {
                if (MessageBox.Show($"是否{tip}当前信息？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    var count = _orderService.ChangeState(id, state);
                    if (count == 0)
                        throw new Exception($"未{tip}任何数据");

                    MessageBox.Show($"{tip}完成！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void ShowImage(object obj)
        {
            string img = (obj as FrameworkElement).Tag.ToString();
            var imgList = (obj as ContentControl).Content as ObservableCollection<OrderImageModel>;

            DialogParameters dps = new DialogParameters();
            dps.Add("img", img);
            dps.Add("imgList", imgList.Select(i => i.ImageName).ToList());
            _dialogService.ShowDialog("ImagePreviewView", dps, null);

            imgList.Move(2, 4);
        }
    }
}
