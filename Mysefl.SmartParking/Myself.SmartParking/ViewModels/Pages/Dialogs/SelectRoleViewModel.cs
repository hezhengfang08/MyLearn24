using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class SelectRoleViewModel : DialogViewModelBase
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
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = "选择角色";

            var model = parameters.GetValue<UserModel>("model");
            _uid = model.UserId;
            var rids = model.Roles.Select(r => r.RoleId).ToList();

            // 获取所有待选角色
            var rs = _roleService.GetRoles("").ToList();
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
                // 这个信息是基于用户来获取
                var user = _userService.Find<SysUser>(_uid);
                if (user == null) return;

                user.Roles.Clear();

                foreach (var role in _roles)
                {
                    if (!role.IsSelected) continue;

                    user.Roles.Add(new RoleUser
                    {
                        RoleId = role.RoleId,
                        UserId = _uid,
                        User = user
                    });
                }
                _userService.Update(user);

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        IRoleService _roleService;
        IUserService _userService;
        public SelectRoleViewModel(IRoleService roleService, IUserService userService)
        {
            _roleService = roleService;
            _userService = userService;
        }
    }
}
