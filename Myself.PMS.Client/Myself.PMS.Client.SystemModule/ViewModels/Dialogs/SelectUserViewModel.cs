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
    public class SelectUserViewModel: DialogViewModelBase
    {
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
        IUserService _userService;
        IRoleService _roleService;
        public SelectUserViewModel(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            this.Title = "选择用户";

            _rid = parameters.GetValue<int>("rid");
            var uids = parameters.GetValue<List<int>>("uids");

            // 从接口获取所有的用户数据
            var us = _userService.GetUsers("").ToList();
            _users = us.Select(u => new UserModel
            {
                IsSelected = uids.Contains(u.eId),
                UserId = u.eId,
                UserName = u.userName,
                RealName = u.realName,
                UserIcon = "http://localhost:5273/api/File/img/" + u.eIcon
            }).ToList();

            _users.ForEach(u => Users.Add(u));
        }
        public override void DoSave()
        {
            try
            {
                List<RoleUser> rusers = new List<RoleUser>();
                foreach (var user in _users)
                {
                    if (!user.IsSelected) continue;

                    rusers.Add(new RoleUser
                    {
                        RoleId = _rid,
                        UserId = user.UserId
                    });
                }
                _roleService.UpdateRoleUsers(rusers.ToArray());

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
