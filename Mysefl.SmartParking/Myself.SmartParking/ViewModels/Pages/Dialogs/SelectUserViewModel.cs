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
    public class SelectUserViewModel: DialogViewModelBase
    {
        IUserService _userService;
        IRoleService _roleService;
        public SelectUserViewModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        private List<UserModel> _users;
        public ObservableCollection<UserModel> Users { get; set; } =
           new ObservableCollection<UserModel>();


        private string _fileterText;

        public string FilterText
        {
            get { return _fileterText; }
            set
            {
                _fileterText = value;

                Users.Clear();
                var us = _users.Where(u =>
                     string.IsNullOrEmpty(value) ||
                     u.RealName.Contains(value) ||
                     u.UserName.Contains(value)
                 ).ToList();
                us.ForEach(u => Users.Add(u));
            }
        }


        private int _rid = 0;
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = "选择用户";

            _rid = parameters.GetValue<int>("rid");
            var uids = parameters.GetValue<List<int>>("uids");
            var us = _userService.GetUsers("").ToList();
            _users = us.Select(u => new UserModel
            {
                IsSelected = uids.Contains(u.UserId),
                UserId = u.UserId,
                UserName = u.UserName,
                RealName = u.RealName,
                UserIcon = "pack://siteoforigin:,,,/Avatars/" + u.UserIcon
            }).ToList();

            _users.ForEach(u => Users.Add(u));
        }
        public override void DoSave()
        {
            try
            {
                // 1. 获取角色
                var role = _roleService.Find<SysRole>(_rid);
                if (role == null) return;

                // 2. 处理当前已选用户
                var selectedUsers = (_users ?? new List<UserModel>())
                    .Where(u => u.IsSelected)
                    .Select(u => new RoleUser
                    {
                        RoleId = _rid,
                        UserId = u.UserId,
                        SysRole = role
                    })
                    .ToList();

                // 3. 更新角色的 Users 集合：如果为 null 则初始化，否则先清空再添加新项
                if (role.Users == null)
                {
                    // 保证赋值类型兼容 ICollection<RoleUser>
                    role.Users = new List<RoleUser>(selectedUsers);
                }
                else
                {
                    role.Users.Clear();
                    foreach (var ru in selectedUsers)
                    {
                        role.Users.Add(ru);
                    }
                }

                // 4. 持久化更新并关闭对话框
                _roleService.Update(role);
                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
