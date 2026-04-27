using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs

{
   public class ModifyDeviceViewModel:DialogViewModelBase   
    {

        IDeviceService _deviceService;
        public ModifyDeviceViewModel(IDeviceService deviceService)
        {
            _deviceService = deviceService;
        }
        public DeviceModel Device { get; set; } =
          new DeviceModel();
        private string _deviceName;
        public string DeviceName
        {
            get { return _deviceName; }
            set
            {
                _deviceName = value;

                this.ErrorList.Clear();
                if (string.IsNullOrEmpty(value))
                {
                    this.ErrorList.Add("DeviceName", new List<string> { "设备名称不能为空" });
                }
                // 关于重复检查   自己实现
                //else if (_deviceService.Query<>)
                //{

                //}
                this.RaiseErrorsChanged();
            }
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<DeviceModel>("model");
            if (model == null)
            {
                this.Title = "新增设备";

                this.DeviceName = "";
            }
            else
            {
                this.Title = "编辑设备";

                var dd = _deviceService.Find<DeviceInfo>(model.DeviceId);
                Device.DeviceId = dd.DeviceId;
                this.DeviceName = dd.DeviceName;
                Device.AddrIp = dd.AddrIp;
                Device.AddrPort = dd.AddrPort;
                Device.AddrCom = dd.AddrCom;
                Device.UserName = dd.UserName;
                Device.Password = dd.Password;
                Device.CallbackUrl1 = dd.CallbackUrl1;
                Device.CallbackUrl2 = dd.CallbackUrl2;
                Device.CallbackUrl3 = dd.CallbackUrl3;
            }
        }
        public override void DoSave()
        {
            if (this.HasErrors) return;

            try
            {
                if (Device.DeviceId == 0)
                {
                    _deviceService.Insert(new DeviceInfo
                    {
                        DeviceName = this.DeviceName,
                        AddrIp = Device.AddrIp,
                        AddrPort = Device.AddrPort,
                        AddrCom = Device.AddrCom,
                        UserName = Device.UserName,
                        Password = Device.Password,
                        CallbackUrl1 = Device.CallbackUrl1,
                        CallbackUrl2 = Device.CallbackUrl2,
                        CallbackUrl3 = Device.CallbackUrl3
                    });
                }
                else
                {
                    var dd = _deviceService.Find<DeviceInfo>(Device.DeviceId);
                    dd.DeviceName = this.DeviceName;
                    dd.AddrIp = Device.AddrIp;
                    dd.AddrPort = Device.AddrPort;
                    dd.AddrCom = Device.AddrCom;
                    dd.UserName = Device.UserName;
                    dd.Password = Device.Password;
                    dd.CallbackUrl1 = Device.CallbackUrl1;
                    dd.CallbackUrl2 = Device.CallbackUrl2;
                    dd.CallbackUrl3 = Device.CallbackUrl3;
                    _deviceService.Update(dd);
                }
                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

      
    }
}
