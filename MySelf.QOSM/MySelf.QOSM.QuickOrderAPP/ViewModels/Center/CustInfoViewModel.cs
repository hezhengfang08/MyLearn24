using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.QuickOrderAPP.Utils;
using Org.BouncyCastle.Asn1.Tsp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Center
{
   public class CustInfoViewModel:ViewModelBase 
    {
        ICustomerService  customerService  = InstanceFactory.CreateInstance<ICustomerService>();
        IOrderService orderService = InstanceFactory.CreateInstance<IOrderService>();
        CustomerInfo customerInfo;

        public CustInfoViewModel()
        {
            InitCustomerInfo();
            IsReadOnly = true;
            isEdit = false;
            InitCommands();
        }
        #region 属性
        public string CustomerName
        {
            get { return customerInfo.CustomerName; }
            set { customerInfo.CustomerName = value; }
        }
        public string CustomerNo
        {
            get { return customerInfo.CustomerNo; }
            set { customerInfo.CustomerNo = value; }
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
                isMale = customerInfo.Sex == "男" ? true : false;
                return isMale;
            }
            set
            {
                isMale = value;
                customerInfo.Sex = isMale ? "男" : "女";
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
                isFemale = customerInfo.Sex == "男" ? false : true;
                return isFemale;
            }
            set
            {
                isFemale = value;
                customerInfo.Sex = isFemale ? "女" : "男";
                isMale = !isFemale;
                OnPropertyChanged();
            }
        }
        public bool? CustomerState
        {
            get { return customerInfo.CustomerState; }
            set { customerInfo.CustomerState = value; }
        }
        public string Phone
        {
            get { return customerInfo.Phone ; }
            set { customerInfo.Phone = value; }
        }
        private bool isPhoneRequired;
        public bool IsPhoneRequired
        {
            get { return isPhoneRequired; }
            set
            {
                isPhoneRequired = value;
                OnPropertyChanged();
            }
        }
        public string Address
        {
            get { return customerInfo.Address; }
            set { customerInfo.Address = value; }
        }

        private bool isAddressRequired;

        public bool IsAddressRequired
        {
            get { return isAddressRequired; }
            set { isAddressRequired = value;
                OnPropertyChanged(); }
        }

        private bool isReadOnly;

        public bool IsReadOnly
        {
            get { return isReadOnly; }
            set { isReadOnly = value;
                OnPropertyChanged();
            }
        }

        private bool isEdit;

        public bool IsEdit
        {
            get { return isEdit; }
            set { isEdit = value;
                OnPropertyChanged();
            }
        }

        #endregion
        #region 命令
        public ICommand SetEditCmd { get; set; }
        public RelayCommand EditCmd { get; set; }   
        public ICommand LogoffCmd { get; set; }
        #endregion
        #region 方法


        private void InitCommands()
        {
            SetEditCmd = new RelayCommand(o =>
            {
                IsEdit = true;
                IsReadOnly = false;
                EditCmd.OnCanExecuteChanged();
            });
            EditCmd = new RelayCommand(o => {
                SaveCustomerInfo();
            }, o =>
            {
                return (!IsReadOnly);
            });
            LogoffCmd = new RelayCommand(o =>
            {
                LogoffCustomer();//客户注销处理
            });
        }
        private void InitCustomerInfo()
        {
            int custId = CommonHelper.loginUser.LoginId;
            if (!CommonHelper.loginUser.IsUser)
            {
                customerInfo = customerService.GetCustomerInfo(custId);
            }
            else
            {
                customerInfo = new  CustomerInfo(); 
            }
        }
        private void SaveCustomerInfo()
        {
            if (!CheckInfo())
            {
                return;
            }
            bool isEdit = customerService.UpdateCustomer(customerInfo);
            if (isEdit) {
                ShowSuccess($"客户:{CustomerNo}  个人信息修改成功！", "客户修改");
                IsReadOnly = true;
                IsEdit = false;
            }
            else
            {
                ShowError($"客户:{CustomerNo} 个人信息修改失败！", "客户修改");
                return;
            }
        }
        private void LogoffCustomer()
        {
            if(ShowQuestion("你确定要注销该客户账号吗？注销成功后将不能再登录该系统","客户注销")== MessageBoxResult.OK)
            {
                if (orderService.IsCustHasUnSignOrder(customerInfo.CustomerId))
                {
                    ShowError("你当前还未完成的订单，不能注销！");
                    return;
                }
                bool blLogoff = customerService.LogoffCustomer(customerInfo.CustomerId);
                if (blLogoff)
                {
                    ShowSuccess($"客户:{CustomerNo}  注销成功！将会自动退出登录", "客户注销");
                    CommonHelper.loginUser = null;
                    CommonHelper.loginRoleId = 0;
                    CommonHelper.existLogin();//退出登录处理
                }
                else
                {
                    ShowError($"客户:{CustomerNo} 注销失败！", "客户注销");
                    return;
                }
            }
        }

        private bool CheckInfo()
        {
            bool check = true;
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
