using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.FeeModule.Models;
using Myself.PMS.Client.IBLL;

namespace Myself.PMS.Client.FeeModule.ViewModels
{
    public class FeeViewModel : PageViewModelBase
    {
        public ObservableCollection<FeeModel> FeeList { get; set; } =
            new ObservableCollection<FeeModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        public DelegateCommand<FeeModel> ConfirmCommand { get; set; }

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

            ConfirmCommand = new DelegateCommand<FeeModel>(DoConfirm);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });


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

                    int index = 0;
                    foreach (var fe in page.Data)
                    {
                        index++;
                        var feeModel = new FeeModel
                        {
                            Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize,
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
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    _feeService.DeleteFee((model as FeeModel).FeeId);

                    MessageBox.Show("删除完成！", "提示");

                    FeeList.Remove(model as FeeModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void DoConfirm(FeeModel fee)
        {
            try
            {
                string tip = fee.State == 0 ? "确认" : "作废";
                if (MessageBox.Show($"是否{tip}当前信息？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    int state = fee.State == 0 ? 1 : -1;

                    var count = _feeService.ChangeState(fee.FeeId, state);
                    if (count == 0)
                        throw new Exception($"未{tip}任何数据");

                    MessageBox.Show($"{tip}完成！", "提示");

                    fee.State = state;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
