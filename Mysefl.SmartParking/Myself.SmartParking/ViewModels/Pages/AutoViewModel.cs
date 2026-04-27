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
    public class AutoViewModel: ViewModelBase<AutoModel>
    {
        IAutoService _autoService;
        IDialogService _dialogService;
        IRegionManager _regionManager;
        public AutoViewModel(IRegionManager regionManager,
            IAutoService autoService,
            IDialogService dialogService) : base(regionManager)
        {
            this.PageTitle = "会员车辆登记";
            _autoService = autoService;
            _dialogService = dialogService;
            _regionManager = regionManager;

            RechargeCommand = new DelegateCommand<AutoModel>(DoRecharge);
            LockAutoCommand = new DelegateCommand<AutoModel>(DoLockAuto);

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            this.Refresh();
        }
        public PaginationModel PaginationModel { get; set; } =
           new PaginationModel();

        public ObservableCollection<AutoModel> AutoList { get; set; } =
            new ObservableCollection<AutoModel>();

        public DelegateCommand<AutoModel> RechargeCommand { get; }
        public DelegateCommand<AutoModel> LockAutoCommand { get; }

        public override void Refresh()
        {
            AutoList.Clear();

            var auto = _autoService.GetAutoList(SearchKey,
                            PaginationModel.PageSize,
                            PaginationModel.PageIndex,
                            out int totalCount);

            int index = 0;
            foreach (var item in auto)
            {
                index++;
                AutoList.Add(new AutoModel
                {
                    Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize,
                    AutoId = item.AutoId,
                    AutoLicense = item.AutoLicense,
                    LColorId = item.LicenseColorId,
                    LColorName = item.LicenseColorName,
                    LTypeId = item.LicenseType,
                    LTypeName = item.LicenseTypeName,
                    AColorId = item.AutoColorId,
                    AColorName = item.AutoColorName,
                    FeeModeId = item.FeeModeId,
                    FeeModeName = item.FeeModeName,
                    Description = item.Description,
                    ValidCount = item.ValidCount,
                    ValidEndTime = string.IsNullOrEmpty(item.ValidEndTime) ? new DateTime() : DateTime.Parse(item.ValidEndTime),
                    ValidStartTime = string.IsNullOrEmpty(item.ValidStartTime) ? new DateTime() : DateTime.Parse(item.ValidStartTime),
                    State = item.State
                });
            }

            // 刷新分页组件的页码
            PaginationModel.FillPageNumbers(totalCount);
        }

        public override void DoModify(AutoModel model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);
            _dialogService.ShowDialog("ModifyAutoView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }

        public override void DoDelete(AutoModel model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _autoService.Delete<AutoRegister>(model.AutoId);

                    MessageBox.Show("删除完成！", "提示");

                    AutoList.Remove(model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        private void DoRecharge(AutoModel model)
        {
            // 需要跳转到RechargeView
            NavigationParameters nps = new NavigationParameters();
            nps.Add("currentUser", CurrentUser);
            nps.Add("auto", model.AutoLicense);
            _regionManager.RequestNavigate("MainRegion", "RechargeView", nps);
        }

        private void DoLockAuto(AutoModel model)
        {
            // 当前记录中的状态信息设置为不可用
            try
            {
                var auto = _autoService.Find<AutoRegister>(model.AutoId);
                if (auto.State == 1)
                {
                    if (MessageBox.Show("是否锁定当前车辆？", "提示", MessageBoxButton.YesNo) ==
                        MessageBoxResult.Yes)
                    {
                        auto.State = 0;
                    }
                }
                else
                {
                    auto.State = 1;
                }
                // 保存到数据库
                _autoService.Update(auto);

                MessageBox.Show("操作已完成！", "提示");

                model.State = auto.State;

                model.LockButtonText = auto.State == 1 ? "锁定" : "启用";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
