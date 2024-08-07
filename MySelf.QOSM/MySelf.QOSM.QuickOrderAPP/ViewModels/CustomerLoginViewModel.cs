using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MySelf.QOSM.BLLFactory;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels
{
    public class CustomerLoginViewModel : ViewModelBase
    {
        public CustomerLoginViewModel()
        {
            ErrorMsg = "";
            ShowErrorMsg = Visibility.Hidden;
            InitCommands();//相关的命令的初始化
        }

        private void InitCommands()
        {
            MinWindowCmd = CreateMinWindowCmd();
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            //登录命令实现
            CustLoginCmd = new RelayCommand(win =>
            {
                CustomerLogin(win);
            }, o =>
            {
                //决定是否执行
                return Check();
            });
            CheckIsLogin = new RelayCommand(o =>
            {
                CustLoginCmd.OnCanExecuteChanged();
            });
            ShowCustRegisterCmd = new RelayCommand(o =>
            {
                ShowCustRegisterWindow();
            });
        }

        #region 属性
        public int CustomerId { get; set; }//客户编号

        private string customerNo;
        //客户号
        public string CustomerNo
        {
            get { return customerNo; }
            set
            {
                customerNo = value;
                OnPropertyChanged();
            }
        }

        private string customerPwd;
        //客户密码
        public string CustomerPwd
        {
            get { return customerPwd; }
            set
            {
                customerPwd = value;
            }
        }

        private string errorMsg;
        //错误信息
        public string ErrorMsg
        {
            get { return errorMsg; }
            set
            {
                errorMsg = value;
                OnPropertyChanged();
            }
        }

        private Visibility showErrorMsg;
        //是否显示错误信息
        public Visibility ShowErrorMsg
        {
            get { return showErrorMsg; }
            set
            {
                showErrorMsg = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 命令
        //登录命令
        public RelayCommand CustLoginCmd { get; set; }
        //是否可登录
        public ICommand CheckIsLogin { get; set; }
        //用户注册命令
        public ICommand ShowCustRegisterCmd { get; set; }
        #endregion

        #region 方法

        private void CustomerLogin(object win)
        {
            ICustomerService customerService = InstanceFactory.CreateInstance<ICustomerService>();

            //调用登录方法
            CustomerInfo custInfo = customerService.CustLogin(CustomerNo, CustomerPwd);
            if (custInfo != null)
            {
                if ((bool)!custInfo.CustomerState)
                {
                    ErrorMsg = "请账号已冻结，不能登录系统！";
                    ShowErrorMsg = Visibility.Visible;
                    return;
                }
                //封装登录者信息
                CommonHelper.loginUser = new LoginInfo()
                {
                    LoginId = custInfo.CustomerId,
                    LoginName = custInfo.CustomerNo,
                    IsUser = false//是客户，不是后台用户
                };
                CustomerId = custInfo.CustomerId;
                CommonHelper.loginRoleId = 1;//客户
                CloseWindow(win);//关闭登录窗口
            }
            else
            {
                ShowError("账号或密码输入有误！", "登录提示");
                return;
            }
        }

        private bool Check()
        {
            bool isLogin = true;
            if (string.IsNullOrEmpty(CustomerNo) || string.IsNullOrEmpty(CustomerPwd))
            {
                isLogin = false;
                if (string.IsNullOrEmpty(CustomerNo))
                {
                    ErrorMsg = "请输入登录账号！";
                }
                else if (string.IsNullOrEmpty(CustomerPwd))
                {
                    ErrorMsg = "请输入登录密码！";
                }
                ShowErrorMsg = Visibility.Visible;
            }
            else
            {
                isLogin = true;
                ShowErrorMsg = Visibility.Hidden;
            }
            return isLogin;
        }

        private void ShowCustRegisterWindow()
        {
            CustomerRegisterViewModel customerRegisterVM = new CustomerRegisterViewModel();
            ShowDialog("custRegister", customerRegisterVM);//显示注册窗口
            if (customerRegisterVM.IsRegisterSuccess)
            {
                CustomerNo = customerRegisterVM.CustomerNo;
            }
        }

        #endregion

    }
}

