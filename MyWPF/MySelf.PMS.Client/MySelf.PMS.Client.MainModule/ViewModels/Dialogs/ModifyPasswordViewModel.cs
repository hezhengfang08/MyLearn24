using MySelf.PMS.Client.IBLL;
using Prism.Commands;
using Prism.Services.Dialogs;
using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace MySelf.PMS.Client.MainModule.ViewModels.Dialogs
{
    public class ModifyPasswordViewModel: IDialogAware, INotifyDataErrorInfo
    {
        public string Title => "密码修改";
        public event Action<IDialogResult> RequestClose;
        public bool CanCloseDialog() => true;
        public void OnDialogClosed()
        {
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
            _uid = parameters.GetValue<int>("id");
            _pwd = parameters.GetValue<string>("pwd");
        }
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;
        public bool HasErrors => ErrorList.Count > 0;
        public IEnumerable GetErrors(string? propertyName)
        {
            if (ErrorList.ContainsKey(propertyName))
                return ErrorList[propertyName];
            return null;
        }
        public Dictionary<string, IList<string>> ErrorList = new Dictionary<string, IList<string>>();
        public void RaiseErrorsChanged([CallerMemberName] string propNmae = "")
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propNmae));
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

                ErrorList.Remove("OldPassword");
                if (string.IsNullOrEmpty(value))
                {
                    ErrorList.Add("OldPassword", new List<string> { "旧密码不能为空" });
                    this.RaiseErrorsChanged();
                }
                else if (value != _pwd)
                {
                    ErrorList.Add("OldPassword", new List<string> { "旧密码不正确" });
                    this.RaiseErrorsChanged();
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
                ErrorList.Remove("NewPassword");
                ErrorList.Remove("ConfirmPassword");
                if (string.IsNullOrEmpty(_oldPassword))
                {
                    ErrorList.Add("NewPassword", new List<string> { "新密码不能为空" });
                    this.RaiseErrorsChanged();
                }
                else if (ConfirmPassword != NewPassword)
                {
                    ErrorList.Add("ConfirmPassword", new List<string> { "两次新密码不一致" });
                    this.RaiseErrorsChanged("ConfirmPassword");
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
                ErrorList.Remove("ConfirmPassword");
                if (string.IsNullOrEmpty(_oldPassword))
                {
                    ErrorList.Add("ConfirmPassword", new List<string> { "确认密码不能为空" });
                    this.RaiseErrorsChanged();
                }
                else if (ConfirmPassword != NewPassword)
                {
                    ErrorList.Add("ConfirmPassword", new List<string> { "两次新密码不一致" });
                    this.RaiseErrorsChanged();
                }
            }
        }

        public DelegateCommand SaveCommand { get; set; }

     

        IUserService _userService;
        public ModifyPasswordViewModel(IUserService userService)
        {
            _userService = userService;

            SaveCommand = new DelegateCommand(DoSave);
        }

        public void DoSave()
        {
            try
            {
                if (_userService.UpdatePassword(_uid, OldPassword, NewPassword))
                    this.RequestClose?.Invoke(new DialogResult(ButtonResult.OK));
                else
                    throw new Exception("用户密码修改失败");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
