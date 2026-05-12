using Myself.PMS.Client.Common;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.SystemModule.Models;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.PMS.Client.SystemModule.ViewModels
{
    public class UserViewModel : PageViewModelBase
    {
        public ObservableCollection<UserModel> Users { get; set; } =
            new ObservableCollection<UserModel>();

        IUserService _userService;
        IRoleService _roleService;
        IDialogService _dialogService;
        public DelegateCommand<object> LockUserCommand { get; set; }

        public UserViewModel(IRegionManager regionManager,
            IUserService userService,
            IRoleService roleService,
            IDialogService dialogService) : base(regionManager)
        {
            PageTitle = "系统用户管理";

            _userService = userService;
            _roleService = roleService;
            _dialogService = dialogService;

            LockUserCommand = new DelegateCommand<object>(DoLockUser);

            this.Refresh();
        }

        public override void Refresh()
        {
            Users.Clear();
            var users = _userService.GetUsers(SearchKey).ToList();

            int index = 1;
            foreach (var user in users)
            {
                UserModel model = new UserModel
                {
                    Index = index++,
                    UserId = user.eId,
                    UserName = user.userName,
                    RealName = user.realName,
                    UserIcon = "http://localhost:7299/api/File/img/" + user.eIcon,
                    Address = user.address,
                    Age = user.age,
                    Password = user.password,
                    Gender = user.gender,
                    Email = user.email,
                    Status = user.status,

                    LockButtonText = user.status == 1 ? "锁定" : "启用"
                };
                Users.Add(model);
            }
        }

        public override void DoModify(object model)
        {
            DialogParameters ps = new DialogParameters();
            ps.Add("model", model);
            _dialogService.ShowDialog("ModifyUserView", ps, result =>
            {
                // 判断子窗口的返回状态，如果OK，刷新当前页面，否则不管
                if (result.Result == ButtonResult.OK)
                {
                    this.Refresh();
                }
            });
        }

        public override void DoDelete(object model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _userService.DeleteUser((model as UserModel).UserId);

                    MessageBox.Show("删除完成！", "提示");

                    Users.Remove(model as UserModel);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        private void DoLockUser(object model)
        {
            try
            {
                var ui = model as UserModel;
                int status = 0; ;
                if (ui.Status == 1)
                {
                    if (MessageBox.Show("是否锁定前用户？", "提示", MessageBoxButton.YesNo) ==
                        MessageBoxResult.Yes)
                    {
                        _userService.LockUser(ui.UserId, 0);
                    }
                }
                else
                {
                    status = 1;
                    _userService.LockUser(ui.UserId, 1);
                }

                MessageBox.Show("操作已完成！", "提示");

                ui.Status = status;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
    }
}
