using Myself.SmartParing.IService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Myself.SmartParking.ViewModels
{
    internal class LoginViewModel : BindableBase, IDialogAware
    {

        public string Title => "用户登录";
        public DialogCloseListener RequestClose { get; }

        public bool CanCloseDialog()
        {
            return true;
        }

        public void OnDialogClosed()
        {
            
        }

        public void OnDialogOpened(IDialogParameters parameters)
        {
           
        }
        private string _userName = "admin";

        public string UserName
        {
            get { return _userName; }
            set { SetProperty<string>(ref _userName, value); }
        }

        private string _password = "123456";

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

       

        private bool _isShowLoading;

        public bool IsShowLoading
        {
            get { return _isShowLoading; }
            set { SetProperty<bool>(ref _isShowLoading, value); }
        }

        private bool _isRecord;
        public bool IsRecord
        {
            get { return _isRecord; }
            set { SetProperty<bool>(ref _isRecord, value); }
        }
        private string _errorInfo;

        public string ErrorInfo
        {
            get { return _errorInfo; }
            set { SetProperty<string>(ref _errorInfo, value); }
        }
        private int _blurValue = 0;

        public int BlurValue
        {
            get { return _blurValue; }
            set { SetProperty<int>(ref _blurValue, value); }
        }


        public DelegateCommand LoginCommand { get; set; }

        IUserService _userService;
        public LoginViewModel(IUserService userService)
        {
            _userService = userService;

            if (File.Exists("temp.txt"))
            {
                string info = File.ReadAllText("temp.txt");
                this.UserName = info.Split("|")[0];
                this.Password = info.Split("|")[1];
            }

            LoginCommand = new DelegateCommand(DoLogin);
        }
        private void ShowLoading(bool status = true)
        {
            if (status)
            {
                IsShowLoading = true;
                this.BlurValue = 5;
            }
            else
            {
                IsShowLoading = false;
                this.BlurValue = 0;
            }
        }
        private void DoLogin()
        {
            ShowLoading();
            ErrorInfo = "";

            Task.Run(async () =>
            {
                //await Task.Delay(3000);
                try
                {
                    var user = _userService.Login(this.UserName, this.Password);
                    if (user == null)
                    {
                        // 提示一下登录   用户名或者密码错误
                        throw new Exception("用户名或者密码错误！");
                    }

                    // 根据选项进行信息记录
                    if (IsRecord)
                    {
                        string info = $"{this.UserName}|{this.Password}";
                        File.WriteAllText("temp.txt", info);
                    }
                    else
                    {
                        if (File.Exists("temp.txt"))
                            File.Delete("temp.text");
                    }
                    // 关闭登录窗口，进入主窗口
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                    });
                }
                catch (Exception ex)
                {
                    ErrorInfo = ex.Message;
                }
                finally
                {
                    ShowLoading(false);
                }
            });
        }
    }
}
