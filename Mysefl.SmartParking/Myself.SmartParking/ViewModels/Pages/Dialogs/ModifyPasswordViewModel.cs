using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using Myself.SmartParking.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels.Pages.Dialogs
{
    public class ModifyPasswordViewModel : DialogViewModelBase,INotifyDataErrorInfo
    {
        IUserService _userService;
        public ModifyPasswordViewModel(IUserService userService)
        {
            _userService = userService;

            this.Title = "修改密码";
        }
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            _pwd = parameters.GetValue<string>("pwd");
            _uid = parameters.GetValue<int>("uid");

            OldPassword = "";
            NewPassword = "";
            ConfirmPassword = "";
        }

        private int _uid;
        private string _pwd;

        private string _oldPassword;

        public string OldPassword
        {
            get { return _oldPassword; }
            set
            {
                _oldPassword = value;

                _errors.Remove("OldPassword");
                if (string.IsNullOrEmpty(value))
                {
                    _errors.Add("OldPassword", new List<string> { "旧密码不能为空" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("OldPassword"));
                }
                else if (value != _pwd)
                {
                    _errors.Add("OldPassword", new List<string> { "旧密码不正确" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("OldPassword"));
                }
            }
        }

        private string _newPassword;

        public string NewPassword
        {
            get { return _newPassword; }
            set
            {
                _newPassword = value;
                _errors.Remove("NewPassword");
                _errors.Remove("ConfirmPassword");
                if (string.IsNullOrEmpty(_oldPassword))
                {
                    _errors.Add("NewPassword", new List<string> { "新密码不能为空" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("NewPassword"));
                }
                else if (ConfirmPassword != NewPassword)
                {
                    _errors.Add("ConfirmPassword", new List<string> { "两次新密码不一致" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ConfirmPassword"));
                }
            }
        }

        private string _confirmPassword;

        public string ConfirmPassword
        {
            get { return _confirmPassword; }
            set
            {
                _confirmPassword = value;
                _errors.Remove("ConfirmPassword");
                if (string.IsNullOrEmpty(_oldPassword))
                {
                    _errors.Add("ConfirmPassword", new List<string> { "确认密码不能为空" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ConfirmPassword"));
                }
                else if (ConfirmPassword != NewPassword)
                {
                    _errors.Add("ConfirmPassword", new List<string> { "两次新密码不一致" });
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("ConfirmPassword"));
                }
            }
        }


        public override void DoSave()
        {
            try
            {
                // 检查旧密码是否正确
                // 检查新密码两次输入是否一致
                if (this.HasErrors) return;

                var user = _userService.Find<SysUser>(_uid);
                if (user == null) return;

                user.Password = NewPassword;
                _userService.Update(user);

                base.DoSave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        Dictionary<string, IList<string>> _errors = new Dictionary<string, IList<string>>();
        public bool HasErrors => _errors.Count > 0;
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (_errors.ContainsKey(propertyName))
                return _errors[propertyName];
            return null;
        }
    }
}
