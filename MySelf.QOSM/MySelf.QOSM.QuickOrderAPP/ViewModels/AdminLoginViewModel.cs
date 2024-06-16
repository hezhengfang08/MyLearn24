using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
   public class AdminLoginViewModel:ViewModelBase 
    {
        public AdminLoginViewModel() {
             ErrorMsg = string.Empty;
            ShowErrorMsg = Visibility.Hidden;
            InitCommands();
        }

        #region 属性
        public int UserId { get;set; }
        private string userName ;

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        private string userPwd;

        public string UserPwd
        {
            get { return userPwd; }
            set { userPwd = value; }
        }
        private string errorMsg;

        public string ErrorMsg
        {
            get { return errorMsg; }
            set { errorMsg = value;
                OnPropertyChanged();    
            }
        }
        private Visibility  showErrorMsg;

        public Visibility ShowErrorMsg
        {
            get { return showErrorMsg; }
            set { showErrorMsg = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region 命令
        public RelayCommand AdminLoginCmd { get; set; }
        public ICommand CheckIsLogin { get; set; }
        #endregion
        #region 方法
        private void InitCommands()
        {
            MinWindowCmd = CreateMinWindowCmd();
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            AdminLoginCmd = new RelayCommand(win =>
            {
                UserLogin(win);
            }, o => { return Check(); });
            CheckIsLogin = new RelayCommand(win => { AdminLoginCmd.OnCanExecuteChanged(); });
        }
        private void UserLogin(object win)
        {
            IUserService userService = InstanceFactory.CreateInstance<IUserService >();
            UserInfo userInfo = userService.AdminLogin(UserName, userPwd);
            if(userInfo != null) { 
                if (!userInfo.UserState)
                {
                    ErrorMsg = "该账号已经冻结，不能登录系统";
                    ShowErrorMsg = Visibility.Visible;
                    return;
                }
                CommonHelper.loginUser = new LoginInfo()
                {
                    LoginId = userInfo.UserId,
                    LoginName = userInfo.UserName,
                    IsUser = true,
                };
                UserId = userInfo.UserId;
                CommonHelper.loginRoleId = userInfo.RoleId;
                CloseWindow(win);
            }
            else
            {
                ErrorMsg = "账号或秘密有误，请重新登录";
                return;
            }
        }
        private bool Check()
        {
            bool isLogin = true;
            if(string.IsNullOrEmpty(UserName)||string.IsNullOrEmpty(UserPwd )) { 
            isLogin = false;
                if (string.IsNullOrEmpty(UserName)){
                    ErrorMsg = "账号不能为空";
                }
                if (string.IsNullOrEmpty(UserPwd))
                {
                    ErrorMsg = "秘密不能为空";
                }
            }
            else
            {
                ShowErrorMsg = Visibility.Hidden;
                isLogin = true;
            }
            return isLogin; 
        }
        #endregion
    }
}
