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
    public class ModifyRoleViewModel:DialogViewModelBase
    {
        IRoleService _roleService;
        public ModifyRoleViewModel(IRoleService roleService)
        {
            _roleService = roleService;
        }
        public int RoleId { get; set; }
        private string _roleName;
        public string RoleName
        {
            get { return _roleName; }
            set
            {
                _roleName = value;

                this.ErrorList.Clear();
                // 不能为空
                if (string.IsNullOrEmpty(value))
                {
                    this.ErrorList.Add("RoleName", new List<string> { "角色名称不能为空" });
                }
                // 不能重复
                if (_roleService.CheckRoleName(value, RoleId))
                {
                    this.ErrorList.Add("RoleName", new List<string> { "角色名称不能重复" });
                }
                this.RaiseErrorsChanged();
            }
        }
        public string RoleDesc { get; set; }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var role = parameters.GetValue<RoleModel>("model");
            if (role == null)
            {
                this.Title = "新增角色权限组";
                RoleName = "";
            }
            else
            {
                this.Title = "编辑角色权限组";
                RoleId = role.RoleId;
                RoleName = role.RoleName;
                RoleDesc = role.RoleDesc;
            }
        }
        public override void DoSave()
        {
            // 这里做一个简单的异常判断，有异常的情况下不执行保存逻辑
            // 当然，可以将这个状态，反馈到按钮上：保存按钮是否可执行
            if (HasErrors) return;

            try
            {
                if (RoleId == 0)
                {
                    _roleService.Insert(new SysRole
                    {
                        RoleName = RoleName,
                        RoleDesc = RoleDesc
                    });
                }
                else
                {
                    var role = _roleService.Find<SysRole>(RoleId);
                    role.RoleName = RoleName;
                    role.RoleDesc = RoleDesc;
                    _roleService.Update(role);
                }
                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
