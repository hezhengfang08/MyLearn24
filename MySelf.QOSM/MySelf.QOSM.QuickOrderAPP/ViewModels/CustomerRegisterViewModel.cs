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
    public class CustomerRegisterViewModel : ViewModelBase
    {
        private CustomerInfo custInfo = null;
        ICustomerService customerService = InstanceFactory.CreateInstance<ICustomerService>();
        public CustomerRegisterViewModel()
        {
            custInfo = new CustomerInfo();
            IsMale = true;
            ShowCNameError = Visibility.Hidden;
            InitCommands();
        }

        private void InitCommands()
        {
            MoveWindowCmd = CreateMoveWindowCmd();
            CloseWindowCmd = CreateCloseWindowCmd();
            RegisterCmd = new RelayCommand(win =>
            {
                SaveCustomerInfo(win);
            });
        }
        #region 属性
        //是否注册成功
        public bool IsRegisterSuccess { get; set; }
        //客户账号
        public string CustomerNo
        {
            get { return custInfo.CustomerNo; }
            set { custInfo.CustomerNo = value; }
        }

        private bool isRequired;
        //账号是否提示必填符号
        public bool IsRequired
        {
            get { return isRequired; }
            set
            {
                isRequired = value;
                OnPropertyChanged();
            }
        }

        private Visibility showCNameError;
        //当客户账号存在时，显示错误信息
        public Visibility ShowCNameError
        {
            get { return showCNameError; }
            set
            {
                showCNameError = value;
                OnPropertyChanged();
            }
        }

        //客户密码
        public string CustomerPwd
        {
            get { return custInfo.CustomerPwd; }
            set { custInfo.CustomerPwd = value; }
        }

        private bool isPRequired;
        //密码是否提示必填符号
        public bool IsPRequired
        {
            get { return isPRequired; }
            set
            {
                isPRequired = value;
                OnPropertyChanged();
            }
        }

        //客户姓名
        public string CustomerName
        {
            get { return custInfo.CustomerName; }
            set { custInfo.CustomerName = value; }
        }

        private bool isNRequired;
        //姓名是否提示必填符号
        public bool IsNRequired
        {
            get { return isNRequired; }
            set
            {
                isNRequired = value;
                OnPropertyChanged();
            }
        }

        private bool isMale;
        //男
        public bool IsMale
        {
            get
            {
                isMale = custInfo.Sex == "男" ? true : false;
                return isMale;
            }
            set
            {
                isMale = value;
                custInfo.Sex = isMale ? "男" : "女";
                isFemale = !isMale;
                OnPropertyChanged();
            }
        }

        private bool isFemale;
        //女
        public bool IsFemale
        {
            get
            {
                isFemale = custInfo.Sex == "男" ? false : true;
                return isFemale;
            }
            set
            {
                isFemale = value;
                custInfo.Sex = isFemale ? "女" : "男";
                isMale = !isFemale;
                OnPropertyChanged();
            }
        }



        //客户电话
        public string Phone
        {
            get { return custInfo.Phone; }
            set { custInfo.Phone = value; }
        }


        private bool isPhoneRequired;
        //电话是否提示必填符号
        public bool IsPhoneRequired
        {
            get { return isPhoneRequired; }
            set
            {
                isPhoneRequired = value;
                OnPropertyChanged();
            }
        }

        //客户地址 
        public string Address
        {
            get { return custInfo.Address; }
            set { custInfo.Address = value; }
        }

        private bool isAddressRequired;
        //地址是否提示必填符号
        public bool IsAddressRequired
        {
            get { return isAddressRequired; }
            set
            {
                isAddressRequired = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region 命令
        public ICommand RegisterCmd { get; set; }

        #endregion

        #region 方法
        //客户注册
        private void SaveCustomerInfo(object win)
        {
            if (!CheckInfo())//检查信息输入
                return;
            //注册
            bool blRegister = customerService.RegisterCustomerInfo(custInfo);//新增
            if (blRegister)
            {
                IsRegisterSuccess = true;//注册成功
                ShowSuccess($"客户:{CustomerNo} 注册成功！确认后方可登录！", "注册提示");
                CloseWindow(win);//关闭注册页

            }
            else
            {
                ShowError($"客户:{CustomerNo} 注册失败！请检查注册信息！", "注册提示");
                return;
            }
        }

        private bool CheckInfo()
        {
            bool check = true;
            if (string.IsNullOrEmpty(CustomerNo))
            {
                IsRequired = true;
                check = false;
            }
            else if (customerService.ExistCustomerNo(CustomerNo))
            {
                ShowCNameError = Visibility.Visible;
                check = false;
            }
            else
            {
                IsRequired = false;
            }
            if (string.IsNullOrEmpty(CustomerPwd))
            {
                IsPRequired = true;
                check = false;
            }
            else
            {
                IsPRequired = false;
            }
            if (string.IsNullOrEmpty(CustomerName))
            {
                IsNRequired = true;
                check = false;
            }
            else
                IsNRequired = false;
            if (string.IsNullOrEmpty(Phone))
            {
                IsPhoneRequired = true;
                check = false;
            }
            else
                IsPhoneRequired = false;
            if (string.IsNullOrEmpty(Address))
            {
                IsAddressRequired = true;
                check = false;
            }
            else
            {
                IsAddressRequired = false;
            }
            return check;
        }
        #endregion
    }
}

