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
    public class ModifyUserViewModel : DialogViewModelBase
    {
        public UserModel UserInfo { get; set; } =
         new UserModel();

        public bool IsReadOnlyUserName { get; set; }
        IUserService _userService;
        public ModifyUserViewModel(IUserService userService)
        {
            _userService = userService;
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

                UserInfo.UserId = model.UserId;
                UserInfo.UserName = model.UserName;
                UserInfo.RealName = model.RealName;
                UserInfo.Gender = model.Gender;
                UserInfo.Address = model.Address;
                UserInfo.Phone = model.Phone;
                UserInfo.Email = model.Email;
                UserInfo.Age = model.Age;
                UserInfo.Password = model.Password;

                string[] temp = model.UserIcon.Split("/");
                UserInfo.UserIcon = temp[temp.Length - 1];
            }
        }
        public override void DoSave()
        {
            if (UserInfo.HasErrors) return;
            // 保存逻辑
            try
            {
                var count = _userService.UpdateUser(new Entities.EmployeeEntity
                {
                    eId = UserInfo.UserId,
                    userName = UserInfo.UserName,
                    realName = UserInfo.RealName,
                    password = UserInfo.Password,
                    eIcon = UserInfo.UserIcon,
                    address = UserInfo.Address,
                    age = (int)UserInfo.Age,
                    status = 1,
                    phone = UserInfo.Phone,
                    gender = (int)UserInfo.Gender,
                });
                if (count == 0)
                    throw new Exception("用户信息更新失败");

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
