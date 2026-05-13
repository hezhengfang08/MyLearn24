using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.SystemModule.Models;
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
using System.Windows.Data;

namespace Myself.PMS.Client.SystemModule.ViewModels
{
    public class BaseInfoViewModel:PageViewModelBase
    {
        public ObservableCollection<BaseInfoModel> BaseInfoList { get; set; } =
          new ObservableCollection<BaseInfoModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();
        IEventAggregator _eventAggregator;
        IBaseInfoService _baseInfoService;
        IDialogService _dialogService;
        public BaseInfoViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IBaseInfoService baseInfoService,
            IDialogService dialogService) : base(regionManager)
        {
            // 允许ObservableCollectin集合跨线程操作
            // 这个除Dispatcher之外的第二种跨线程处理方案
            BindingOperations.EnableCollectionSynchronization(BaseInfoList, this);

            this.PageTitle = "公告与通知";

            _eventAggregator = eventAggregator;
            _baseInfoService = baseInfoService;
            _dialogService = dialogService;

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            this.Refresh();
        }
        public override void Refresh()
        {
            BaseInfoList.Clear();
            _eventAggregator.GetEvent<LoadingEvent>()
                .Publish("正在加载....");

            Task.Run(() =>
            {
                try
                {
                    var page = _baseInfoService.GetInfoPage(this.SearchKey,
                        PaginationModel.PageIndex, PaginationModel.PageSize);

                    int index = 0;
                    foreach (var item in page.Data)
                    {
                        index++;
                        // 数据
                        BaseInfoModel model = new BaseInfoModel();

                        model.Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize;
                        model.InfoId = item.InfoId;
                        model.InfoHeader = item.InfoHeader;
                        model.InfoContent = item.InfoContent;
                        model.InfoKey = item.InfoKey;
                        model.ModifyTime = item.ModifyTime;
                        model.PublishTime = item.PublishTime;
                        model.InfoType = item.InfoType;
                        model.State = item.state;
                        model.UserName = item.UserName;

                        // 有前面的BindingOperations.EnableCollectionSynchronization方法调用
                        // 这里不需要Dispatcher
                        //Application.Current.Dispatcher.Invoke(() =>
                        //{
                        BaseInfoList.Add(model);
                        //});
                    }
                    Application.Current.Dispatcher?.Invoke(() =>
                    {
                        // 刷新分页组件的页码
                        PaginationModel.FillPageNumbers(page.TotalCount);
                    });
                }
                catch { }
                finally
                {
                    _eventAggregator.GetEvent<LoadingEvent>()
                        .Publish("");
                }
            });
        }
        public override void DoModify(object model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);

            _dialogService.ShowDialog("ModifyInfoView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }
        public override void DoDelete(object model)
        {
            base.DoDelete(model);
        }
    }
}
