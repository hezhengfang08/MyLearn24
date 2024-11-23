using Myself.SmartParing.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.ViewModels
{
    internal class LoginViewModel : BindableBase, IDialogAware
    {
     
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
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { SetProperty<string>(ref _userName, value); }
        }

        private string _password = "";

        public string Password
        {
            get { return _password; }
            set { SetProperty<string>(ref _password, value); }
        }

        private bool _isRecord;

        public bool IsRecord
        {
            get { return _isRecord; }
            set { SetProperty<bool>(ref _isRecord, value); }
        }

        public DelegateCommand LoginCommand { get; set; }

        IUserService _userService;
        public LoginViewModel(IUserService userService)
        {
            _userService = userService;

            LoginCommand = new DelegateCommand(DoLogin);
        }

        private void DoLogin()
        {
            _userService.Login(this.UserName, this.Password);
        }
    }
}
