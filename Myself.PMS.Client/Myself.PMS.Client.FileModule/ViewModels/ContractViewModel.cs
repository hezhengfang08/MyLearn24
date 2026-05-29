using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.FileModule.Models;
using Myself.PMS.Client.IBLL;
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

namespace Myself.PMS.Client.FileModule.ViewModels
{
    public class ContractViewModel : PageViewModelBase
    {
        public ObservableCollection<ContractModel> ContractList { get; set; } =
            new ObservableCollection<ContractModel>();

        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DelegateCommand<ContractModel> RevokeCommand { get; set; }
        public DelegateCommand<ContractModel> ApproveCommand { get; set; }
        public DelegateCommand<ContractModel> ExecuteCommand { get; set; }
        public DelegateCommand<ContractModel> ArchivedCommand { get; set; }

        IContractService _contractService;
        IDialogService _dialogService;
        GlobalValues _globalValues;
        public ContractViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IContractService contractService,
            IDialogService dialogService,
            GlobalValues globalValues) : base(regionManager, eventAggregator)
        {
            BindingOperations.EnableCollectionSynchronization(ContractList, this);
            this.PageTitle = "合同管理";

            _contractService = contractService;
            _dialogService = dialogService;
            _globalValues = globalValues;

            this.StartDate = DateTime.Now.AddDays(-15);
            this.EndDate = DateTime.Now;

            RevokeCommand = new DelegateCommand<ContractModel>(DoRevoke);
            ApproveCommand = new DelegateCommand<ContractModel>(DoApprove);
            ExecuteCommand = new DelegateCommand<ContractModel>(DoExecute);
            ArchivedCommand = new DelegateCommand<ContractModel>(DoArchived);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            this.Refresh();
        }

        public override void Refresh()
        {
            ContractList.Clear();
            this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    var page = _contractService.GetDatas(this.SearchKey,
                                this.StartDate, this.EndDate,
                                PaginationModel.PageIndex, PaginationModel.PageSize);

                    int index = 0;
                    foreach (var item in page.Data)
                    {
                        index++;
                        ContractModel model = new ContractModel();
                        model.Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize;
                        model.ContractId = item.ContractId;
                        model.ContractName = item.ContractName;
                        model.ContractNumber = item.ContractNumber;
                        model.ContractAmount = item.ContractAmount;
                        model.ExcuteAmount = item.ExcuteAmount;
                        model.SignTime = item.SignTime;
                        model.Operator = item.Operator;
                        model.Opposite = item.Opposite;
                        model.State = item.State;
                        model.ArchivedTime = item.ArchivedTime;
                        model.ArchivedUserId = item.ArchivedUserId;
                        model.ArchivedUserName = item.ArchivedUserName;
                        model.ModifyTime = item.ModifyTime;
                        model.Userid = item.Userid;
                        model.UserName = item.UserName;

                        model.Prograss = model.ContractAmount == 0 ? 0 : model.ExcuteAmount / model.ContractAmount;

                        ContractList.Add(model);
                    }

                    Application.Current.Dispatcher?.Invoke(() =>
                    {
                        // 刷新分页组件的页码
                        PaginationModel.FillPageNumbers(page.TotalCount);
                    });
                }
                catch (Exception ex)
                {

                }
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
            _dialogService.ShowDialog("ModifyContractView", dps, result =>
            {
                if (result.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        public override void DoDelete(object model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    _contractService.DeleteInfo((model as ContractModel).ContractId);

                    MessageBox.Show("删除完成！", "提示");

                    ContractList.Remove(model as ContractModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void DoRevoke(ContractModel model)
        {
            // 撤销
            if (ChangeState(model.ContractId, 0, "撤销"))
                model.State = 0;
        }
        private void DoApprove(ContractModel model)
        {
            // 审批
            if (ChangeState(model.ContractId, 1, "审批"))
                model.State = 1;
        }

        private bool ChangeState(int id, int state, string tip)
        {
            try
            {
                if (MessageBox.Show($"是否{tip}当前信息？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    var count = _contractService.ChangeState(id, state);
                    if (count == 0)
                        throw new Exception($"未{tip}任何数据");

                    MessageBox.Show($"{tip}完成！", "提示");
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
                return false;
            }
        }

        private void DoExecute(ContractModel model)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("model", model);
            _dialogService.ShowDialog("ExecuteView", dps, result =>
            {
                if (result.Result == ButtonResult.OK)
                    this.Refresh();
            });
        }

        private void DoArchived(ContractModel model)
        {
            try
            {
                if (MessageBox.Show("是否继续执行建档？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    ContractEntity entity = new ContractEntity();
                    entity.ContractId = model.ContractId;
                    entity.ArchivedTime = DateTime.Now;
                    entity.ArchivedUserId = _globalValues.UserId;
                    entity.ArchivedUserName = _globalValues.UserName;

                    var count = _contractService.Archived(entity);

                    MessageBox.Show("建档完成！", "提示");

                    model.State = 3;
                    model.ArchivedTime = entity.ArchivedTime;
                    model.ArchivedUserId = entity.ArchivedUserId;
                    model.ArchivedUserName = entity.ArchivedUserName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
