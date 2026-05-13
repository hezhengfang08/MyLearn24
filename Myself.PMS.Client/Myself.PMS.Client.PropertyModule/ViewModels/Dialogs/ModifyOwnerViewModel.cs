using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.PropertyModule.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.PropertyModule.ViewModels.Dialogs
{
    public class ModifyOwnerViewModel : DialogViewModelBase
    {
        public OwnerModel OwnerInfo { get; set; } =
            new OwnerModel();

        public QuarterModel CurrentQuarter { get; set; }
        public List<QuarterModel> QuarterList { get; set; }

        public BuildingModel CurrentBuilding { get; set; }
        public List<BuildingModel> BuildingList { get; set; }

        IOwnerService _ownerService;
        GlobalValues _globalValues;
        public ModifyOwnerViewModel(IOwnerService ownerService, GlobalValues globalValues)
        {
            _ownerService = ownerService;
            _globalValues = globalValues;

            // 初始化 
            // 对应的集合    当前选中项
            QuarterList = ownerService.GetQuarters()
                .Select(q => new QuarterModel { Id = q.Id, Name = q.Name })
                .ToList();

            BuildingList = ownerService.GetBuildings()
                .Select(q => new BuildingModel { Id = q.Id, Name = q.Name, Qid = q.Qid })
                .ToList();
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<OwnerModel>("model");
            if (model == null)
            {
                Title = "新增业主信息";

                OwnerInfo.State = 0;
                OwnerInfo.Gender = 0;
                OwnerInfo.UserId = _globalValues.UserId;
                OwnerInfo.UserName = _globalValues.UserName;
            }
            else
            {
                Title = "编辑业主信息";

                OwnerInfo.OwnerId = model.OwnerId;
                OwnerInfo.HouseHolder = model.HouseHolder;
                OwnerInfo.IdNumber = model.IdNumber;
                OwnerInfo.Phone = model.Phone;
                OwnerInfo.Bid = model.Bid;
                OwnerInfo.Bname = model.Bname;
                OwnerInfo.Qid = model.Qid;
                OwnerInfo.Qname = model.Qname;
                OwnerInfo.RoomNum = model.RoomNum;
                OwnerInfo.Gender = model.Gender;
                OwnerInfo.CredentialImg1 = model.CredentialImg1;
                OwnerInfo.CredentialImg2 = model.CredentialImg2;
                OwnerInfo.Description = model.Description;
                OwnerInfo.State = model.State;
                OwnerInfo.ModifyTime = model.ModifyTime;
                OwnerInfo.UserId = model.UserId;
                OwnerInfo.UserName = model.UserName;

            }
        }

        public override void DoSave()
        {
            base.DoSave();
        }
    }
}
