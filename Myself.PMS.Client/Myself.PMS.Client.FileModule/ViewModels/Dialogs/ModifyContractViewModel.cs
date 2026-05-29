using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.FileModule.Models;
using Myself.PMS.Client.IBLL;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.FileModule.ViewModels.Dialogs
{
    public class ModifyContractViewModel : DialogViewModelBase
    {
        public ContractModel ContractInfo { get; set; } =
            new ContractModel();

        IContractService _contractService;
        GlobalValues _globalValues;
        public ModifyContractViewModel(IContractService contractService,
            GlobalValues globalValues)
        {
            _contractService = contractService;
            _globalValues = globalValues;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<ContractModel>("model");
            if (model == null)
            {
                this.Title = "新增合同信息";

                ContractInfo.ContractNumber = "";
                ContractInfo.ContractName = "";
                ContractInfo.ContractAmount = 0;
                ContractInfo.SignTime = DateTime.Now;
                ContractInfo.Opposite = "";
                ContractInfo.Operator = "";
                ContractInfo.State = 0;
            }
            else
            {
                this.Title = "编辑合同信息";

                ContractInfo.ContractId = model.ContractId;
                ContractInfo.ContractNumber = model.ContractNumber;
                ContractInfo.ContractName = model.ContractName;
                ContractInfo.ContractAmount = model.ContractAmount;
                ContractInfo.SignTime = model.SignTime;
                ContractInfo.Opposite = model.Opposite;
                ContractInfo.Operator = model.Operator;
                ContractInfo.State = model.State;
            }
        }

        public override void DoSave()
        {
            if (ContractInfo.HasErrors) return;

            try
            {
                ContractEntity entity = new ContractEntity();
                entity.ContractId = ContractInfo.ContractId;
                entity.ContractNumber = ContractInfo.ContractNumber;
                entity.ContractName = ContractInfo.ContractName;
                entity.ContractAmount = ContractInfo.ContractAmount;
                entity.SignTime = ContractInfo.SignTime;
                entity.Opposite = ContractInfo.Opposite;
                entity.Operator = ContractInfo.Operator;
                entity.State = ContractInfo.State;

                entity.ModifyTime = DateTime.Now;
                entity.Userid = _globalValues.UserId;
                entity.UserName = _globalValues.UserName;

                var count = _contractService.UpdateInfo(entity);
                if (count == 0)
                    throw new Exception("数据失败失败");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show("数据保存失败！" + ex.Message, "提示");
            }
        }
    }
}
