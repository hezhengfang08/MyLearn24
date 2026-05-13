using Myself.PMS.Client.Common;
using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.SystemModule.Models;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.SystemModule.ViewModels.Dialogs
{
    public class SelectRoleViewModel:  DialogViewModelBase
    {
        private List<RoleModel> _roles;
        public ObservableCollection<RoleModel> Roles { get; set; } =
           new ObservableCollection<RoleModel>();

        private string _fileterText;

        public string FilterText
        {
            get { return _fileterText; }
            set
            {
                _fileterText = value;

                Roles.Clear();
                var us = _roles.Where(u =>
                     string.IsNullOrEmpty(value) ||
                     u.RoleName.Contains(value) ||
                     u.RoleDesc.Contains(value)
                 ).ToList();
                us.ForEach(u => Roles.Add(u));
            }
        }
            private int _uid = 0;

        IRoleService _roleService;
        IUserService _userService;
        public SelectRoleViewModel(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = "选择角色";

            var model = parameters.GetValue<UserModel>("model");
            _uid = model.UserId;
            var rids = model.Roles.Select(r => r.RoleId).ToList();

            // 获取所有待选角色
            var rs = _roleService.GetAllRoles().ToList();
            _roles = rs.Select(r => new RoleModel
            {
                IsSelected = rids.Contains(r.RoleId),
                RoleId = r.RoleId,
                RoleName = r.RoleName,
                RoleDesc = r.RoleDesc
            }).ToList();

            _roles.ForEach(r => Roles.Add(r));
        }
        public override void DoSave()
        {
            try
            {
                List<RoleUser> roles = new List<RoleUser>();
                foreach (var role in _roles)
                {
                    if (!role.IsSelected) continue;

                    roles.Add(new RoleUser
                    {
                        RoleId = role.RoleId,
                        UserId = _uid
                    });
                }
                var count = _userService.SaveUserRoles(roles.ToArray());
                if (count == 0)
                    throw new Exception("用户角色信息更新失败！");

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
