using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.SM
{

  public  class RoleInfoViewModel:InfoVMBase<RoleInfo>
    {

        IRoleService roleService = InstanceFactory.CreateInstance<IRoleService>();

        RoleInfo roleInfo = null;
        string oldName = "";

        public RoleInfoViewModel(int actType, RoleInfo role)
        {
            ActType = actType;
            ShowRNameError = Visibility.Collapsed;
            if(ActType == 1) {
                roleInfo = new RoleInfo();
                SubmitText = "新增";
                IsRequired = true;
            }
            else
            {
                roleInfo = role;
                SubmitText = "修改";
                oldName = role.RoleName;
            }
            InitCommands();
        }
        #region 属性
        private bool isRequired;
        //角色名称是否提示必填符号
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                OnPropertyChanged();
            }
        }

        private Visibility showRNameError;
        //当角色名称已存在时，显示错误信息
        public Visibility ShowRNameError
        {
            get { return showRNameError; }
            set
            {
                showRNameError = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 编号
        /// </summary>
        public int RoleId
        {
            get { return roleInfo.RoleId; }
            set
            {
                roleInfo.RoleId = value;
            }
        }
        //角色名称
        public string RoleName
        {
            get { return roleInfo.RoleName; }
            set { roleInfo.RoleName = value; }
        }
        //描述

        public string Remark
        {
            get { return roleInfo.Remark; }
            set { roleInfo.Remark = value; }
        }
        #endregion
        #region 方法
        private void SaveRole(object win)
        {
            string conType = ActType == 1 ? "新增" : "修改";
            string msgTitle = $"角色{conType}";
            //检查信息
            if (string.IsNullOrEmpty(RoleName))
            {
                IsRequired = true;
                return;
            }
            if (ActType == 1 || (ActType == 2 && RoleName != oldName))
            {
                if (roleService.ExistRole(RoleName))
                {
                    ShowRNameError = Visibility.Visible;
                    return;
                }
            }
            //提交处理
            bool blConfirm = roleService.SaveRole(roleInfo);
            if (blConfirm)
            {
                ShowSuccess($"角色：{RoleName} {conType}成功！", msgTitle);
                //刷新
                OnReloadList(roleInfo);
                //关闭窗口
                CloseWindow(win);
            }
            else
            {
                ShowError($"角色：{RoleName} {conType}失败！", msgTitle);
                return;
            }
        }
        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            ConfirmCmd = new RelayCommand
           (win =>
           {
               SaveRole(win);
           });
        }
        #endregion
    }
}
