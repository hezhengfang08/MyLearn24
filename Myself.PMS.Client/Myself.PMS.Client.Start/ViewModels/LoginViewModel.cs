using Myself.PMS.Client.IBLL;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.Start.ViewModels
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


        public DelegateCommand LoginCommand { get; set; }


        IUserService _userService;
        public LoginViewModel(IUserService userService,
            IFileService fileService)
        {
            _userService = userService;

            LoginCommand = new DelegateCommand(DoLogin);


            // 检查应用更新
            //1、获取最新文件列表
            // 
            var files = fileService.GetUpgradeFiles();
            // 2、文件判断，新增的直接下载；更新的直接下载；删除的直接删除
            //    客户端本地需要一个记录，最后更新的记录（）
        }


        private void DoLogin()
        {
            // 将用户名和密码提交到WebApi，检查状态，将状态写入State属性
            State = _userService.Login(UserName, Password);
        }


    }
}
