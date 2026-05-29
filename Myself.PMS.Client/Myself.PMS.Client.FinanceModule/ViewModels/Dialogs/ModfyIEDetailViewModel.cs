using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.FinanceModule.Models;
using Myself.PMS.Client.IBLL;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.FinanceModule.ViewModels.Dialogs
{
    public class ModfyIEDetailViewModel : DialogViewModelBase
    {
        public IEModel IEInfo { get; set; } = new IEModel();

        private int _dataType = 1;

        public int DataType
        {
            get { return _dataType; }
            set
            {
                _dataType = value;
                TypeList.Clear();
                if (value == 1)
                {
                    TypeList.Add("日常经营收入");
                    TypeList.Add("预收水电充值收入");
                    TypeList.Add("采暖和空调收入");
                    TypeList.Add("车位费收入");
                    TypeList.Add("场地服务收入");
                    TypeList.Add("押金收入");
                    TypeList.Add("其他收入");
                }
                else
                {
                    TypeList.Add("员工薪酬支出");
                    TypeList.Add("交流通讯支出");
                    TypeList.Add("学习培训支出");
                    TypeList.Add("其他支出");
                }

                CurrentType = TypeList[0];
                this.RaisePropertyChanged("CurrentType");
                this.RaisePropertyChanged("DataType");
            }
        }

        public string CurrentType { get; set; }
        public ObservableCollection<string> TypeList { get; set; } = new ObservableCollection<string>();

        public decimal Amount { get; set; }
        public string AmountDesc { get; set; }


        IFinanceService _financeService;
        GlobalValues _globalValues;
        public ModfyIEDetailViewModel(IFinanceService financeService,
            GlobalValues globalValues)
        {
            _financeService = financeService;
            _globalValues = globalValues;
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<IEModel>("model");
            if (model == null)
            {
                this.Title = "新增收支记录";

                DataType = 1;
                IEInfo.HappendTime = DateTime.Now;
            }
            else
            {
                this.Title = "编辑收支记录";

                IEInfo.IEID = model.IEID;
                DataType = model.RecordType;
                IEInfo.HappendTime = model.HappendTime;
                Amount = model.RecordType == 1 ? model.IncomeAmount : model.ExpensesAmount;
                CurrentType = model.RecordType == 1 ? model.IncomeType : model.ExpensesType;
                AmountDesc = model.RecordType == 1 ? model.IncomeDesc : model.ExpensesDesc;
                IEInfo.ProjectName = model.ProjectName;
                IEInfo.Account = model.Account;
                IEInfo.Department = model.Department;
                IEInfo.Operator = model.Operator;
                IEInfo.Description = model.Description;
                IEInfo.RecordId = model.RecordId;
                IEInfo.RecordName = model.RecordName;
                IEInfo.RecordTime = model.RecordTime;
                IEInfo.State = model.State;
                IEInfo.ModifyTime = model.ModifyTime;
                IEInfo.UserId = model.UserId;
                IEInfo.UserName = model.UserName;
            }
        }

        public override void DoSave()
        {
            // 关于数据的校验   自行处理

            try
            {
                IEEntity entity = new IEEntity();
                entity.IEID = IEInfo.IEID;
                entity.HappendTime = IEInfo.HappendTime;
                entity.RecordType = DataType;
                entity.Amount = Amount;
                entity.AmountType = CurrentType;
                entity.AmountDesc = AmountDesc;
                entity.ProjectName = IEInfo.ProjectName;
                entity.Account = IEInfo.Account;
                entity.Department = IEInfo.Department;
                entity.Operator = IEInfo.Operator;
                entity.Description = IEInfo.Description;
                entity.RecordId = IEInfo.RecordId;
                entity.RecordName = IEInfo.RecordName;
                entity.RecordTime = IEInfo.RecordTime;
                entity.State = IEInfo.State;
                entity.ModifyTime = DateTime.Now;
                entity.UserId = _globalValues.UserId;
                entity.UserName = _globalValues.UserName;

                int count = _financeService.UpdateInfo(entity);
                if (count == 0)
                    throw new Exception("收支数据更新失败");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
