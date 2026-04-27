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
  public  class ModifyRechargeViewModel: DialogViewModelBase
    {
        IRechargeService _rechargeService;
        public ModifyRechargeViewModel(IRechargeService rechargeService)
        {
            _rechargeService = rechargeService;

            AutoList = _rechargeService.Query<AutoRegister>(q => q.State == 1).Select(a => a.AutoLicense).ToList();
            FeeModeList = _rechargeService.Query<BaseFeeMode>(q => true).ToList();
        }
        public RechargeModel RechargeInfo { get; set; } =
          new RechargeModel();
        public List<string> AutoList { get; set; } = new List<string>();
        public List<BaseFeeMode> FeeModeList { get; set; } = new List<BaseFeeMode>();
        int _userId;
        string _userName;
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = "会员续费登记";
            RechargeInfo.RechargeEndTime = DateTime.Now;

            var user = parameters.GetValue<UserModel>("user");
            if (user != null)
            {
                _userId = user.UserId;
                _userName = user.UserName;
            }
        }
        public override void DoSave()
        {
            try
            {
                _rechargeService.Insert(new MemberRecharge
                {
                    AutoLisence = RechargeInfo.AutoLisence,
                    FeeModeId = RechargeInfo.FeeModeId,
                    RechargeCount = RechargeInfo.RechargeCount,
                    RechargeEndTime = RechargeInfo.RechargeEndTime.ToString(),
                    State = 1,
                    CreateTime = DateTime.Now.ToString(),
                    CreateId = _userId,
                    CreateName = _userName
                });



                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
