using Myself.SmartParing.IService;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Myself.SmartParking.Models
{
    public class UserModel:BindableBase, INotifyDataErrorInfo
    {
        public UserModel() { }

        IUserService _userService;
        public UserModel(IUserService userService)
        {
            _userService = userService;
        }
        public int Index { get; set; }

        public int UserId { get; set; }

        private bool _isSelected;

        public bool IsSelected
        {
            get { return _isSelected; }
            set { SetProperty<bool>(ref _isSelected, value); }
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;

                _errors.Remove("UserName");
                Task.Run(async () =>
                {
                    await Task.Delay(500);// 只是做为耗时操作的测试，实际开发中不需要这句

                    if (string.IsNullOrEmpty(value))
                    {
                        _errors.Add("UserName", new List<string> { "用户名不能为空" });
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("UserName"));
                    }
                    else if (_userService != null && _userService.CheckUserName(value))
                    {
                        _errors.Add("UserName", new List<string> { "用户名已被占用" });
                        ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs("UserName"));
                    }
                });
            }
        }

        public string RealName { get; set; }
        public string UserIcon { get; set; }
        public int? Age { get; set; }
        private int? _gender;
        public int? Gender
        {
            get { return _gender; }
            set { SetProperty<int?>(ref _gender, value); }
        }

        public string Address { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        private int _status;

        public int Status
        {
            get { return _status; }
            set { SetProperty<int>(ref _status, value); }
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
