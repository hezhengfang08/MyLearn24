using MySelf.QOSM.Common.Encrypt;
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

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Center
{
    public class ModifyCustPwdViewModel : ViewModelBase
    {
        ICustomerService customerService = InstanceFactory.CreateInstance<ICustomerService>();
        int custId = CommonHelper.loginUser.LoginId;//客户编号
        public ModifyCustPwdViewModel()
        {
            CustomerNo = CommonHelper.loginUser.LoginName;
            InitCommands();
        }

        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            CheckPwdCmd = new RelayCommand(o =>
            {
                CheckIsModify();
                ModifyPwdCmd.OnCanExecuteChanged();
            });
            ModifyPwdCmd = new RelayCommand(win =>
            {
                UpdateLoginPwd(win);
            }, o =>
            {
                return isCanModify;
            });
        }

        public bool isModified = false;//标识密码是否已修改
        private bool isCanModify = false;//是否可提交修改
        #region 属性
        public string CustomerNo { get; set; }
        public string CustomerPwd { get; set; }
        public string NewPwd { get; set; }
        public string ConfirmPwd { get; set; }

        private bool isPRequired;
        //原始密码是否必填
        public bool IsPRequired
        {
            get { return isPRequired; }
            set
            {
                isPRequired = value;
                OnPropertyChanged();
            }
        }

        private bool isNewPRequired;
        //新密码是否必填
        public bool IsNewPRequired
        {
            get { return isNewPRequired; }
            set
            {
                isNewPRequired = value;
                OnPropertyChanged();
            }
        }

        private bool isConRequired;
        //确认密码是否必填
        public bool IsConRequired
        {
            get { return isConRequired; }
            set
            {
                isConRequired = value;
                OnPropertyChanged();
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
        //显示异常消息
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
        public RelayCommand ModifyPwdCmd { get; set; }
        public ICommand CheckPwdCmd { get; set; }
        #endregion

        #region 方法
        private void CheckIsModify()
        {
            if (string.IsNullOrEmpty(CustomerPwd) || string.IsNullOrEmpty(NewPwd) || string.IsNullOrEmpty(ConfirmPwd))
            {
                isCanModify = false;//修改按钮不可用
            }
            if (string.IsNullOrEmpty(CustomerPwd))
            {
                IsPRequired = true;
            }
            else
                IsPRequired = false;

            if (string.IsNullOrEmpty(NewPwd))
            {
                IsNewPRequired = true;
            }
            else
                IsNewPRequired = false;

            if (string.IsNullOrEmpty(ConfirmPwd))
            {
                IsConRequired = true;
            }
            else
                IsConRequired = false;

            //信息检查
            string enoldPwd = MD5Encrypt.Encrypt(CustomerPwd);//加密原始密码
            CustomerInfo cust = customerService.GetCustomerInfo(custId);
            if (cust.CustomerPwd != enoldPwd)
            {
                ErrorMsg = "原始密码输入错误！";
                ShowErrorMsg = Visibility.Visible;
                return;
            }
            else if (NewPwd == CustomerPwd)
            {
                ErrorMsg = "新密码与原始密码不能相同！";
                ShowErrorMsg = Visibility.Visible;
                return;
            }
            else if (NewPwd != ConfirmPwd)
            {
                ErrorMsg = "两次密码输入不一致！";
                ShowErrorMsg = Visibility.Visible;
                return;
            }
            else
            {
                isCanModify = true;//可修改
                ShowErrorMsg = Visibility.Hidden;
            }


        }

        private void UpdateLoginPwd(object win)
        {
            bool blUpdate = customerService.UpdateCustomerPwd(custId, NewPwd);
            if (blUpdate)
            {
                ShowSuccess("你的登录密码修改成功！请重新登录！", "修改密码");
                isModified = true;//已修改密码
                CloseWindow(win);
            }
            else
            {
                ShowError("密码修改失败！", "修改密码");
                return;
            }
        }
        #endregion
    }
}

