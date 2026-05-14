using Myself.PMS.Client.Common;
using Myself.PMS.Client.Controls;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.PropertyModule.Models;
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

namespace Myself.PMS.Client.PropertyModule.ViewModels
{
    public class OwnerViewModel: PageViewModelBase
    {
        public ObservableCollection<OwnerModel> Owners { get; set; } =
      new ObservableCollection<OwnerModel>();
        public List<ConditionModel> Conditions { get; set; } =
           new List<ConditionModel>();
        public PaginationModel PaginationModel { get; set; } =
          new PaginationModel();
        IOwnerService _ownerService;
        IDialogService _dialogService;
        public OwnerViewModel(IRegionManager regionManager, IEventAggregator eventAggregator,
            IOwnerService ownerService,
            IDialogService dialogService)
            : base(regionManager, eventAggregator)
        {
            BindingOperations.EnableCollectionSynchronization(Owners, this);

            this.PageTitle = "业主信息登记";

            _ownerService = ownerService;
            _dialogService = dialogService;

            PaginationModel.NavCommand = new DelegateCommand<object>(index =>
            {
                PaginationModel.PageIndex = int.Parse(index.ToString());
                this.Refresh();
            });

            // 初始化检索条件
            Conditions.Add(new ConditionModel { Name = "HouseHolder", Header = "业主姓名" });
            Conditions.Add(new ConditionModel { Name = "RoomNum", Header = "房间信息" });
            Conditions.Add(new ConditionModel { Name = "IdNumber", Header = "身份证号" });
            Conditions.Add(new ConditionModel { Name = "Phone", Header = "手机号" });

            this.Refresh();
        }
        public override void Refresh()
        {
            Owners.Clear();
            this.BeginLoading();

            Task.Run(() =>
            {
                try
                {
                    // 动态条件进行查询
                    // 分页
                    List<ConditionEntity> conditions = new List<ConditionEntity>();
                    foreach (var item in Conditions)
                    {
                        if (item.Value != null && !string.IsNullOrEmpty(item.Value.ToString()))
                            conditions.Add(new ConditionEntity { Name = item.Name, Value = item.Value });
                    }

                    var os = _ownerService.GetOwners(
                        conditions.ToArray(),
                        PaginationModel.PageIndex,
                        PaginationModel.PageSize);

                    int index = 0;
                    foreach (var o in os.Data)
                    {
                        index++;
                        Owners.Add(new OwnerModel
                        {
                            Index = index + (PaginationModel.PageIndex - 1) * PaginationModel.PageSize,
                            OwnerId = o.OwnerId,
                            HouseHolder = o.HouseHolder,
                            IdNumber = o.IdNumber,
                            Phone = o.Phone,
                            Bid = o.Bid,
                            Bname = o.Bname,
                            Qid = o.Qid,
                            Qname = o.Qname,
                            RoomNum = o.RoomNum,
                            Gender = o.Gender,
                            CredentialImg1 = o.CredentialImg1,
                            CredentialImg2 = o.CredentialImg2,
                            Description = o.Description,
                            State = o.State,
                            ModifyTime = o.ModifyTime,
                            UserId = o.OwnerId,
                            UserName = o.UserName,
                        });
                    }

                    // 刷新分页组件的页码
                    PaginationModel.FillPageNumbers(os.TotalCount);
                }
                catch { }
                finally { this.EndLoading(); }
            });
        }
        public override void DoModify(object model)
        {
            DialogParameters dps = new DialogParameters();
            dps.Add("model", model);
            _dialogService.ShowDialog("ModifyOwnerView", dps, result =>
            {
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }
        public override void DoDelete(object model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    _ownerService.DeleteOwner((model as OwnerModel).OwnerId);

                    MessageBox.Show("删除完成！", "提示");

                    Owners.Remove(model as OwnerModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
