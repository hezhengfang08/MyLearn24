using Myself.SmartParing.IService;
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
    public class DeviceViewModel:ViewModelBase<DeviceModel>
    {
        public ObservableCollection<DeviceModel> Devices { get; set; } =
           new ObservableCollection<DeviceModel>();

        IDeviceService _deviceService;
        IDialogService _dialogService;
        public DeviceViewModel(
          IRegionManager regionManager,
          IDeviceService deviceService,
          IDialogService dialogService)
          : base(regionManager)
        {
            this.PageTitle = "设备信息维护";

            _deviceService = deviceService;
            _dialogService = dialogService;

            this.Refresh();
        }
        public override void Refresh()
        {
            Devices.Clear();

            var ds = _deviceService.Query<DeviceInfo>(

                d =>
                string.IsNullOrEmpty(SearchKey) ||
                d.DeviceName.Contains(SearchKey)

                );

            int index = 1;
            foreach (var item in ds)
            {
                Devices.Add(new DeviceModel
                {
                    Index = index++,
                    DeviceId = item.DeviceId,
                    DeviceName = item.DeviceName,
                    AddrIp = item.AddrIp,
                    AddrPort = item.AddrPort,
                    AddrCom = item.AddrCom,
                    UserName = item.UserName,
                    Password = item.Password,
                    CallbackUrl1 = item.CallbackUrl1,
                    CallbackUrl2 = item.CallbackUrl2,
                    CallbackUrl3 = item.CallbackUrl3
                });
            }
        }
        public override void DoModify(DeviceModel model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);
            _dialogService.ShowDialog("ModifyDeviceView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }
        public override void DoDelete(DeviceModel model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _deviceService.Delete<DeviceInfo>(model.DeviceId);

                    MessageBox.Show("删除完成！", "提示");

                    Devices.Remove(model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
