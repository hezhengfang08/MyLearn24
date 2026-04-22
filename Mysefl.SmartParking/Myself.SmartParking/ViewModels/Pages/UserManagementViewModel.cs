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

namespace Myself.SmartParking.ViewModels.Pages
{
    public class UserManagementViewModel: ViewModelBase<UserModelEx>
    {
        IUserService _userService;
        IDialogService _dialogService;
        public UserManagementViewModel(IRegionManager regionManager,
            IUserService userService,
            IDialogService dialogService)
            : base(regionManager)
        {
            PageTitle = "系统用户管理";

            _userService = userService;
            _dialogService = dialogService; 

            ResetPasswordCommand = new DelegateCommand<UserModelEx>(DoResetPassword);
            LockUserCommand = new DelegateCommand<UserModelEx>(DoLockUser);

            this.Refresh();
    }
        public ObservableCollection<UserModelEx> Users { get; set; } =
           new ObservableCollection<UserModelEx>();


        public override void Refresh()
        {
            Users.Clear();
            var users = _userService.GetUsers(SearchKey);

            int index = 1;
            foreach (var user in users)
            {
                Users.Add(new UserModelEx
                {
                    Index = index++,
                    UserId = user.UserId,
                    UserName = user.UserName,
                    RealName = user.RealName,
                    UserIcon = "pack://siteoforigin:,,,/Avatars/" + user.UserIcon,
                    Address = user.Address,
                    Age = user.Age,
                    Password = user.Password,
                    Gender = user.Gender,
                    Email = user.Email,
                    Status = user.Status,

                    LockButtonText = user.Status == 1 ? "锁定" : "启用"
                });
            }
        }
        public override void DoDelete(UserModelEx model)
        {
            try
            {
                if (MessageBox.Show("是否确定删除此项？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    // 物理删除
                    _userService.Delete<SysUser>(model.UserId);

                    MessageBox.Show("删除完成！", "提示");

                    Users.Remove(model);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        public override void DoModify(UserModelEx model)
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
        public void DoResetPassword(UserModelEx model)
        {
            try
            {
                if (MessageBox.Show("是否确定重置当前用户密码？", "提示", MessageBoxButton.YesNo) ==
                    MessageBoxResult.Yes)
                {
                    var user = _userService.Find<SysUser>(model.UserId);
                    user.Password = "123456";
                    _userService.Update<SysUser>(user);

                    MessageBox.Show("重置完成！", "提示");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }
        public void DoLockUser(UserModelEx ui)
        {
            try
            {
              
                var user = _userService.Find<SysUser>(ui.UserId);
                if (ui.Status == 1)
                {
                    if (MessageBox.Show("是否锁定前用户？", "提示", MessageBoxButton.YesNo) ==
                        MessageBoxResult.Yes)
                    {
                        user.Status = 0;
                    }
                }
                else
                {
                    user.Status = 1;
                }
                _userService.Update<SysUser>(user);

                MessageBox.Show("操作已完成！", "提示");

                ui.Status = user.Status;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "提示");
            }
        }

        // 在类中添加 ResetPasswordCommand 属性
        public DelegateCommand<UserModelEx> ResetPasswordCommand { get; private set; }
        public DelegateCommand<UserModelEx> LockUserCommand { get; private set; }
    }
}
