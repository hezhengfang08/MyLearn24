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
    public class ModifyUserViewModel : DialogViewModelBase
    {
        IUserService _userService;
        public ModifyUserViewModel(IUserService userService)
        {
            _userService = userService;
        }
        public UserModel UserInfo { get; set; } =
          new UserModel();
        public bool IsReadOnlyUserName
        {
            get; set;
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<UserModel>("model");
            if (model == null)
            {
                Title = "新增用户信息";

                UserInfo = new UserModel(_userService);
                UserInfo.UserName = "";
                UserInfo.Gender = 1;
                UserInfo.Password = "123456";
                UserInfo.UserIcon = "a01.jpg";
            }
            else
            {
                Title = "编辑菜单项";

                IsReadOnlyUserName = true;

                var su = _userService.Find<SysUser>(model.UserId);
                UserInfo.UserId = su.UserId;
                UserInfo.UserName = su.UserName;
                UserInfo.RealName = su.RealName;
                UserInfo.Password = su.Password;
                UserInfo.UserIcon = su.UserIcon;
                UserInfo.Gender = su.Gender;
                UserInfo.Address = su.Address;
                UserInfo.Age = su.Age;
                UserInfo.Status = su.Status;
                UserInfo.Phone = su.Phone;
            }
        }
        public override void DoSave()
        {
            if (UserInfo.HasErrors) return;
            // 保存逻辑
            try
            {
                if (UserInfo.UserId == 0)
                {
                    // 新增保存
                    // 
                    // UserId的编码规则：
                    // 4位年份   2024
                    // 3位序号   XXX
                    int uid = DateTime.Now.Year * 1000;
                    //
                    int num = _userService.GetUsers("").Max(u => u.UserId) % uid;
                    uid += num + 1;

                    _userService.Insert<SysUser>(new SysUser
                    {
                        UserId = uid,
                        UserName = UserInfo.UserName,
                        RealName = UserInfo.RealName,
                        Password = UserInfo.Password,
                        UserIcon = UserInfo.UserIcon,
                        Address = UserInfo.Address,
                        Age = UserInfo.Age,
                        Status = 1,
                        Phone = UserInfo.Phone,
                        Gender = UserInfo.Gender,
                    });
                }
                else
                {
                    // 编辑保存

                    var user = _userService.Find<SysUser>(UserInfo.UserId);
                    user.RealName = UserInfo.RealName;
                    user.Gender = UserInfo.Gender;
                    user.Address = UserInfo.Address;
                    user.Age = UserInfo.Age;
                    user.Phone = UserInfo.Phone;
                    // 还可以添加页面中可以进行修改的字段属性
                    _userService.Update(user);
                }

                // 保存成功后 ，需要关闭当前弹窗
                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


    }
}
