using Myself.SmartParing.IService;
using Myself.SmartParking.Controls;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages
{
    public class RechargeViewModel: ViewModelBase<RechargeModel>
    {
        IRechargeService _rechargeService;
        IDialogService _dialogService;
        public RechargeViewModel(IRegionManager regionManager,
            IRechargeService rechargeService,
            IDialogService dialogService)
            : base(regionManager)
        {
            this.PageTitle = "会员续费记录";

            _rechargeService = rechargeService;
            _dialogService = dialogService;

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

        }
        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        public ObservableCollection<RechargeModel> RechargeList { get; set; } =
               new ObservableCollection<RechargeModel>();
        UserModel _currentUser;
        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            _currentUser = navigationContext.Parameters.GetValue<UserModel>("user");

            // 从车辆 记录过来的时候，正常情况是有值的
            // 还有一种情况，从主页面菜单过来的时候，这里面是没有auto参数
            // 不管哪种情况 ，这里逻辑都正常
            SearchKey = navigationContext.Parameters.GetValue<string>("auto");
            this.RaisePropertyChanged(nameof(SearchKey));

            this.Refresh();
        }

        public override void Refresh()
        {
            RechargeList.Clear();
            var rl = _rechargeService.GetRecharges(
                    SearchKey,
                    PaginationModel.PageSize,
                    PaginationModel.PageIndex,
                    out int totalCount
                );

            int index = 0;
            foreach (var r in rl)
            {
                RechargeList.Add(new RechargeModel
                {
                    Index = ++index,
                    RId = r.RId,
                    AutoLisence = r.AutoLisence,
                    FeeModeId = r.FeeModeId,
                    FeeModeName = r.FeeModeName,
                    RechargeCount = r.RechargeCount,
                    RechargeEndTime = string.IsNullOrEmpty(r.RechargeEndTime) ? null : DateTime.Parse(r.RechargeEndTime),
                    RechargeStartTime = string.IsNullOrEmpty(r.RechargeStartTime) ? null : DateTime.Parse(r.RechargeStartTime),
                    CreateId = r.CreateId,
                    CreateName = r.CreateName,
                    CreateTime = string.IsNullOrEmpty(r.CreateTime) ? null : DateTime.Parse(r.CreateTime),
                    State = r.State,
                    IsCanCancel = r.State == 1
                });
            }


            PaginationModel.FillPageNumbers(totalCount);
        }

        public override void DoModify(RechargeModel model)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("user", _currentUser);
            _dialogService.ShowDialog("ModifyRechargeView", dps, result =>
            {
             
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }

        public override void DoDelete(RechargeModel rechargeModel)
        {
            try
            {
                if (MessageBox.Show("是否作废当前记录？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                   
                    var recharge = _rechargeService.Find<MemberRecharge>(rechargeModel.RId);
                    recharge.State = 0;
                    // 物理删除
                    _rechargeService.Update(recharge);

                    MessageBox.Show("操作已完成！", "提示");

                    rechargeModel.State = 0;
                    rechargeModel.IsCanCancel = false;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
