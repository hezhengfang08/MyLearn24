using Myself.PMS.Client.Common;
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
    public class ModifyRoleViewModel : DialogViewModelBase
    {
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
                if (!string.IsNullOrEmpty(value) &&
                    _roleService.CheckRoleName(value, RoleId))
                {
                    this.ErrorList.Add("RoleName", new List<string> { "角色名称不能重复" });
                }
                this.RaiseErrorsChanged();
            }
        }
        public string RoleDesc { get; set; }

        IRoleService _roleService;
        public ModifyRoleViewModel(IRoleService roleService)
        {
            _roleService = roleService;
        }
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
            if (this.HasErrors) return;

            try
            {
                var count = _roleService.UpdateRole(new Entities.SysRole
                {
                    RoleId = this.RoleId,
                    RoleName = this.RoleName,
                    RoleDesc = this.RoleDesc,
                    State = 1
                });
                if (count == 0)
                    throw new Exception("角色信息更新失败");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
