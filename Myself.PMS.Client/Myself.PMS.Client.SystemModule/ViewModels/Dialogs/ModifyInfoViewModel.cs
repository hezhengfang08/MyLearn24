using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.SystemModule.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.SystemModule.ViewModels.Dialogs
{
    public class ModifyInfoViewModel : DialogViewModelBase
    {
        public int InfoId { get; set; }
        private string _infoTitle;
        public string InfoTitle
        {
            get { return _infoTitle; }
            set
            {
                SetProperty<string>(ref _infoTitle, value, () =>
                {
                    // 不能为空
                    if (string.IsNullOrEmpty(value))
                    {
                        this.ErrorList.Add("InfoTitle", new List<string> { "发布标题不能为空" });
                    }
                    else
                        this.ErrorList.Remove("InfoTitle");
                    this.RaiseErrorsChanged();
                });
            }
        }
        private int _infoType;

        public int InfoType
        {
            get { return _infoType; }
            set { SetProperty<int>(ref _infoType, value); }
        }
        private string _key;

        public string Key
        {
            get { return _key; }
            set
            {
                SetProperty<string>(ref _key, value, () =>
                {
                    // 不能为空
                    if (string.IsNullOrEmpty(value))
                    {
                        this.ErrorList.Add("Key", new List<string> { "关键词不能为空" });
                    }
                    else
                        this.ErrorList.Remove("Key");
                    this.RaiseErrorsChanged();
                });
            }
        }
        private string _content;

        public string Content
        {
            get { return _content; }
            set
            {
                SetProperty<string>(ref _content, value, () =>
                {
                    // 不能为空
                    if (string.IsNullOrEmpty(value))
                    {
                        this.ErrorList.Add("Content", new List<string> { "发布内容不能为空" });
                    }
                    else
                        this.ErrorList.Remove("Content");
                    this.RaiseErrorsChanged();
                });
            }
        }
        IBaseInfoService _baseInfoService;
        GlobalValues _globalValues;
        public ModifyInfoViewModel(IBaseInfoService baseInfoService, GlobalValues globalValues)
        {
            _baseInfoService = baseInfoService;
            _globalValues = globalValues;
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<BaseInfoModel>("model");
            if (model == null)
            {
                this.Title = "新消息发布";
                InfoType = 1;
                InfoTitle = "";
                Content = "";
                Key = "";
            }
            else
            {
                this.Title = "编辑信息";
                InfoId = model.InfoId;
                InfoTitle = model.InfoHeader;
                InfoType = model.InfoType;
                Content = model.InfoContent;
                Key = model.InfoKey;
            }
        }

        public override void DoSave()
        {
            if (this.HasErrors) return;

            try
            {
                BaseInfo baseInfo = new BaseInfo();
                baseInfo.InfoId = InfoId;
                baseInfo.InfoHeader = InfoTitle;
                baseInfo.InfoType = InfoType;
                baseInfo.InfoContent = Content;
                baseInfo.InfoKey = Key;
                baseInfo.ModifyTime = DateTime.Now;
                baseInfo.UserId = _globalValues.UserId;
                baseInfo.UserName = _globalValues.UserName;

                var count = _baseInfoService.UpdateInfo(baseInfo);
                if (count == 0)
                    throw new Exception("信息更新失败");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
