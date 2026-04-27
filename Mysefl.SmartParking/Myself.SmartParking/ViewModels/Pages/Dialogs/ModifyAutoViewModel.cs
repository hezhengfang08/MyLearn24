using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
   public class ModifyAutoViewModel: DialogViewModelBase
    {

        IAutoService _autoService;
        public ModifyAutoViewModel(IAutoService autoService)
        {
            _autoService = autoService;

            // 待选列表
            LicenseColorList = _autoService.Query<BaseLicenseColor>(q => true).ToList();
            LicenseTypeList = _autoService.Query<BaseLicenseType>(q => true).ToList();
            AutoColorList = _autoService.Query<BaseAutoColor>(q => true).ToList();
            FeeModeList = _autoService.Query<BaseFeeMode>(q => true).ToList();
        }
        public AutoModel Auto { get; set; } =
          new AutoModel();
        public List<BaseLicenseColor> LicenseColorList { get; set; }
        public List<BaseLicenseType> LicenseTypeList { get; set; }
        public List<BaseAutoColor> AutoColorList { get; set; }
        public List<BaseFeeMode> FeeModeList { get; set; }

        private string _autoLisence;

        // 必输项、不重复
        public string AutoLicense
        {
            get { return _autoLisence; }
            set
            {
                _autoLisence = value;

                this.ErrorList.Clear();
                if (string.IsNullOrEmpty(value))
                {
                    this.ErrorList.Add("AutoLicense", new List<string> { "车牌号不能为空" });
                }
                else if (_autoService.Query<AutoRegister>(a => a.AutoLicense == value &&
                                                               a.AutoId != Auto.AutoId).Count() > 0)
                {
                    this.ErrorList.Add("AutoLicense", new List<string> { "车牌号不能重复" });
                }
                this.RaiseErrorsChanged();
            }
        }
        private bool _isAutoLicenseReadOnly = false;

        public bool IsAutoLicenseReadOnly
        {
            get { return _isAutoLicenseReadOnly; }
            set { SetProperty<bool>(ref _isAutoLicenseReadOnly, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<AutoModel>("model");
            if (model == null)
            {
                this.Title = "登记车辆信息";

                this.AutoLicense = "";
                this.Auto.ValidEndTime = DateTime.Now;
            }
            else
            {
                this.Title = "编辑车辆信息";
                IsAutoLicenseReadOnly = true;

                var auto = _autoService.Find<AutoRegister>(model.AutoId);
                Auto.AutoId = auto.AutoId;
                this.AutoLicense = auto.AutoLicense;
                Auto.LColorId = auto.LicenseColorId;
                Auto.LTypeId = auto.LicenseType;
                auto.AutoColorId = auto.AutoColorId;
                Auto.FeeModeId = auto.FeeModeId;
                Auto.ValidEndTime = string.IsNullOrEmpty(auto.ValidEndTime) ? new DateTime() : DateTime.Parse(auto.ValidEndTime);
                Auto.ValidCount = auto.ValidCount;
                Auto.Description = auto.Description;
            }
        }

        public override void DoSave()
        {
            if (this.HasErrors) return;
            try
            {
                if (Auto.AutoId == 0)
                {
                    _autoService.Insert(new AutoRegister
                    {
                        AutoLicense = this.AutoLicense,
                        LicenseColorId = Auto.LColorId,
                        LicenseType = Auto.LTypeId,
                        AutoColorId = Auto.AColorId,
                        FeeModeId = Auto.FeeModeId,
                        ValidEndTime = Auto.ValidEndTime.ToString("yyyy-MM-dd HH:mm:ss"),
                        ValidCount = Auto.ValidCount,
                        Description = Auto.Description
                    });
                }
                else
                {
                    var auto = _autoService.Find<AutoRegister>(Auto.AutoId);
                    auto.AutoLicense = this.AutoLicense;
                    auto.LicenseColorId = Auto.LColorId;
                    auto.LicenseType = Auto.LTypeId;
                    auto.AutoColorId = Auto.AColorId;
                    auto.FeeModeId = Auto.FeeModeId;
                    auto.ValidEndTime = Auto.ValidEndTime.ToString("yyyy-MM-dd HH:mm:ss");
                    auto.ValidCount = Auto.ValidCount;
                    auto.Description = Auto.Description;
                    _autoService.Update(auto);
                }

                base.DoSave();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
