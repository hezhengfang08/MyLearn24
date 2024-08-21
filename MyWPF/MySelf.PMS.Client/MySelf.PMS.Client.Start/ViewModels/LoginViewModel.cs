using MySelf.PMS.Client.Entities;
using MySelf.PMS.Client.IBLL;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MySelf.PMS.Client.Start.ViewModels
{
    public class LoginViewModel : BindableBase, IDialogAware
    {
        public string Title => "登录";
        public event Action<IDialogResult> RequestClose;
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



        public string UserName { get; set; } = "admin";
        public string Password { get; set; } = "123456";
        private bool _state;

        public bool State
        {
            get { return _state; }
            set { SetProperty<bool>(ref _state, value); }
        }

        private string _errorInfo;

        public string ErrorInfo
        {
            get { return _errorInfo; }
            set { SetProperty<string>(ref _errorInfo, value); }
        }


        public DelegateCommand LoginCommand { get; set; }

        public DelegateCommand<object> LoadedCommand { get; set; }


        IUserService _userService;
        GlobalValues _globalValues;
        public LoginViewModel(IUserService userService,
            IFileService fileService, GlobalValues globalValues)
        {
            _userService = userService;
            _globalValues = globalValues;

            LoginCommand = new DelegateCommand(DoLogin);
            LoadedCommand = new DelegateCommand<object>(obj =>
            {
                FrameworkElement element = obj as FrameworkElement;
                VisualStateManager.GoToElementState(element, "ShowLoginState", true);
            });





        }


        private void DoLogin()
        {
            // 将用户名和密码提交到WebApi，检查状态，将状态写入State属性
            // 需要返回用户相关信息  Token
            try
            {
                var user = _userService.Login(UserName, Password);
                // 登录成功

                _globalValues.Token = user.Token;

                   DialogParameters dps = new DialogParameters();
                dps.Add("user", user);
                RequestClose?.Invoke(new DialogResult(ButtonResult.OK, dps));
            }
            catch (Exception ex)
            {
                this.ErrorInfo = ex.Message;
            }
        }


    }
}

