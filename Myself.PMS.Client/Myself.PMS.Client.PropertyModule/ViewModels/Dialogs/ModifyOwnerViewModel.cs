using Microsoft.Win32;
using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.PropertyModule.Models;
using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.PropertyModule.ViewModels.Dialogs
{
    public class ModifyOwnerViewModel : DialogViewModelBase
    {
        public OwnerModel OwnerInfo { get; set; } =
            new OwnerModel();

        //public QuarterModel CurrentQuarter { get; set; }
        private QuarterModel _currentquarter;

        public QuarterModel CurrentQuarter
        {
            get { return _currentquarter; }
            set
            {
                _currentquarter = value;

                // 进行楼栋的过滤
                BuildingList = _buildingList.Where(b => b.Qid == value.Id)
                    .Select(b => new BuildingModel
                    {
                        Id = b.Id,
                        Qid = b.Qid,
                        Name = b.Name,
                    }).ToList();
                CurrentBuilding = BuildingList?[0];

                this.RaisePropertyChanged(nameof(CurrentBuilding));
                this.RaisePropertyChanged(nameof(BuildingList));
            }
        }
        public List<QuarterModel> QuarterList { get; set; }

        public BuildingModel CurrentBuilding { get; set; }
        private List<BuildingEntity> _buildingList;
        public List<BuildingModel> BuildingList { get; set; }
        public DelegateCommand<string> SelectCommand { get; set; }

        IOwnerService _ownerService;
        GlobalValues _globalValues;
        IFileService _fileService;
        public ModifyOwnerViewModel(IOwnerService ownerService, GlobalValues globalValues, IFileService fileService)
        {
            _ownerService = ownerService;
            _globalValues = globalValues;
            _fileService = fileService;
            // 初始化 
            // 对应的集合    当前选中项
            QuarterList = ownerService.GetQuarters()
                .Select(q => new QuarterModel { Id = q.Id, Name = q.Name })
                .ToList();

            //BuildingList = ownerService.GetBuildings()
            //    .Select(q => new BuildingModel { Id = q.Id, Name = q.Name, Qid = q.Qid })
            //    .ToList();
            _buildingList = ownerService.GetBuildings().ToList();
            SelectCommand = new DelegateCommand<string>(DoSelect);
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<OwnerModel>("model");
            if (model == null)
            {
                Title = "新增业主信息";

                OwnerInfo.HouseHolder = "";
                OwnerInfo.RoomNum = "";
                OwnerInfo.Phone = "";
                OwnerInfo.IdNumber = "";
                OwnerInfo.State = 0;
                OwnerInfo.Gender = 0;
                OwnerInfo.UserId = _globalValues.UserId;
                OwnerInfo.UserName = _globalValues.UserName;

                CurrentQuarter = QuarterList?[0];
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
                OwnerInfo.CredentialImg1 = "https://localhost:7299/api/File/idcard/" + model.CredentialImg1;
                OwnerInfo.CredentialImg2 = "https://localhost:7299/api/File/idcard/" + model.CredentialImg2;
                OwnerInfo.Description = model.Description;
                OwnerInfo.State = model.State;
                OwnerInfo.ModifyTime = model.ModifyTime;
                OwnerInfo.UserId = model.UserId;
                OwnerInfo.UserName = model.UserName;

                CurrentQuarter = QuarterList?.FirstOrDefault(q => q.Id == model.Qid);
                CurrentBuilding = BuildingList?.FirstOrDefault(b => b.Id == model.Bid);


                imgName1 = model.CredentialImg1;
                imgName2 = model.CredentialImg2;
            }
        }
        string imgName1, imgName2;

        public override void DoSave()
        {
            if (OwnerInfo.HasErrors) return;
            try
            {
                // 两个图片的处理
                // 需要一个图片上传的接口，先执行上传，然后再执行数据保存
                if (imgChanged1)
                {
                    imgName1 = OwnerInfo.IdNumber + "_A" + Path.GetExtension(OwnerInfo.CredentialImg1);
                    _fileService.UploadIdCard(OwnerInfo.CredentialImg1, imgName1);
                    imgChanged1 = false;
                }
                if (imgChanged2)
                {
                    imgName2 = OwnerInfo.IdNumber + "_B" + Path.GetExtension(OwnerInfo.CredentialImg1);
                    _fileService.UploadIdCard(OwnerInfo.CredentialImg2, imgName2);
                    imgChanged2 = false;
                }

                OwnerEntity ownerInfo = new OwnerEntity()
                {
                    OwnerId = OwnerInfo.OwnerId,
                    HouseHolder = OwnerInfo.HouseHolder,
                    IdNumber = OwnerInfo.IdNumber,
                    Phone = OwnerInfo.Phone,
                    Bid = CurrentBuilding.Id,
                    Bname = CurrentBuilding.Name,
                    Qid = CurrentQuarter.Id,
                    Qname = CurrentQuarter.Name,
                    RoomNum = OwnerInfo.RoomNum,
                    Gender = OwnerInfo.Gender,
                    CredentialImg1 = imgName1,
                    CredentialImg2 = imgName2,
                    Description = OwnerInfo.Description,
                    State = OwnerInfo.State,
                    ModifyTime = DateTime.Now,
                    UserId = OwnerInfo.UserId,
                    UserName = OwnerInfo.UserName,
                };

                var count = _ownerService.UpdateOwner(ownerInfo);
                if (count == 0)
                    throw new Exception("信息更新失败");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        bool imgChanged1 = false, imgChanged2 = false;
        private void DoSelect(string flag)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "图片文件|*.jpg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == true)
            {
                if (flag == "1")
                {
                    OwnerInfo.CredentialImg1 = openFileDialog.FileName;
                    imgChanged1 = true;
                }
                else if (flag == "2")
                {
                    OwnerInfo.CredentialImg2 = openFileDialog.FileName;
                    imgChanged2 = true;
                }
            }
        }
    }
}
