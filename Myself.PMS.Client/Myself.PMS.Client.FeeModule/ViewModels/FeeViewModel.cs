using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.FeeModule.Models;
using Myself.PMS.Client.IBLL;
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

namespace Myself.PMS.Client.FeeModule.ViewModels
{
    public class FeeViewModel: PageViewModelBase
    {
        public ObservableCollection<FeeModel> FeeList { get; set; } =
         new ObservableCollection<FeeModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();
        IFeeService _feeService;
        IDialogService _dialogService;
        public FeeViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IFeeService feeService,
            IDialogService dialogService)
            : base(regionManager, eventAggregator)
        {
            BindingOperations.EnableCollectionSynchronization(FeeList, this);

            this.PageTitle = "费用代收代记";

            _feeService = feeService;
            _dialogService = dialogService;

            this.Refresh();
        }
        public override void Refresh()
        {
            FeeList.Clear();
            this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    var page = _feeService.GetFeePage(this.SearchKey,
                        PaginationModel.PageIndex, PaginationModel.PageSize);

                    foreach (var fe in page.Data)
                    {
                        var feeModel = new FeeModel
                        {
                            FeeId = fe.FeeId,
                            FeeMode = fe.FeeMode,
                            FeeModeId = fe.FeeModeId,
                            Amount = fe.Amount,
                            BId = fe.BId,
                            BName = fe.BName,
                            QId = fe.QId,
                            QName = fe.QName,
                            RoomNumber = fe.RoomNumber,
                            Description = fe.Description,
                            State = fe.State,
                            UserId = fe.UserId,
                            UserName = fe.UserName,
                            ModifyTime = fe.ModifyTime,
                            ValidTime = fe.ValidTime,
                        };
                        FeeList.Add(feeModel);
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
                    this.EndLoading();
                }

            });
        }
        public override void DoModify(object model)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("model", model);
            _dialogService.ShowDialog("ModifyFeeView", dps, result =>
            {
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
