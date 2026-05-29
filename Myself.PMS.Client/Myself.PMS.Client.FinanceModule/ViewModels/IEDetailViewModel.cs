using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.FinanceModule.Models;
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

namespace Myself.PMS.Client.FinanceModule.ViewModels
{
    public class IEDetailViewModel : PageViewModelBase
    {
        public ObservableCollection<IEModel> IEList { get; set; } =
            new ObservableCollection<IEModel>();


        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();


        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DelegateCommand<IEModel> ConfirmCommand { get; set; }
        public DelegateCommand<IEModel> RevokeCommand { get; set; }

        IFinanceService _financeService;
        IDialogService _dialogService;
        public IEDetailViewModel(IRegionManager regionManager,
            IEventAggregator eventAggregator,
            IFinanceService financeService,
            IDialogService dialogService) : base(regionManager, eventAggregator)
        {
            BindingOperations.EnableCollectionSynchronization(IEList, this);

            this.PageTitle = "收支明细";

            _financeService = financeService;
            _dialogService = dialogService;

            StartDate = DateTime.Now.AddDays(-15);
            EndDate = DateTime.Now;

            ConfirmCommand = new DelegateCommand<IEModel>(DoConfirm);
            RevokeCommand = new DelegateCommand<IEModel>(DoRevoke);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            this.Refresh();
        }

        public override void Refresh()
        {
            IEList.Clear();
            this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    var page = _financeService.GetDatas(this.SearchKey,
                          this.StartDate, this.EndDate,
                          PaginationModel.PageIndex, PaginationModel.PageSize);

                    int index = 0;
                    foreach (var item in page.Data)
                    {
                        index++;
                        IEModel model = new IEModel()
                        {
                            Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize,
                            IEID = item.IEID,
                            HappendTime = item.HappendTime,
                            RecordType = item.RecordType,
                            IncomeAmount = item.RecordType == 1 ? item.Amount : 0,
                            IncomeType = item.RecordType == 1 ? item.AmountType : "",
                            IncomeDesc = item.RecordType == 1 ? item.AmountDesc : "",
                            ExpensesAmount = item.RecordType == 2 ? item.Amount : 0,
                            ExpensesType = item.RecordType == 2 ? item.AmountType : "",
                            ExpensesDesc = item.RecordType == 2 ? item.AmountDesc : "",
                            ProjectName = item.ProjectName,
                            Account = item.Account,
                            Department = item.Department,
                            Operator = item.Operator,
                            Description = item.Description,
                            RecordId = item.RecordId,
                            RecordName = item.RecordName,
                            RecordTime = item.RecordTime,
                            State = item.State,
                        };
                        IEList.Add(model);
                    }

                    Application.Current.Dispatcher?.Invoke(() =>
                    {
                        // 刷新分页组件的页码
                        PaginationModel.FillPageNumbers(page.TotalCount);
                    });
                }
                catch
                {
                    // 这里的异常会被忽略掉，不会有任何提醒
                    // 正常来讲  肯定是需要做异常提醒的
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
            _dialogService.ShowDialog("ModfyIEDetailView", dps, result =>
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
                    _financeService.DeleteInfo((model as IEModel).IEID);

                    MessageBox.Show("删除完成！", "提示");

                    IEList.Remove(model as IEModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void DoConfirm(IEModel model)
        {
            // 状态写1
            ChangeState(model.IEID, 1, "建档");
            model.State = 1;
        }

        private void DoRevoke(IEModel model)
        {
            // 状态写0
            ChangeState(model.IEID, 0, "撤销");
            model.State = 0;
        }

        private void ChangeState(int id, int state, string tip)
        {
            try
            {
                if (MessageBox.Show($"是否{tip}当前信息？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    _financeService.ChangeState(id, state);

                    MessageBox.Show($"{tip}完成！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
