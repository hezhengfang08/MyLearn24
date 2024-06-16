using MySelf.QOSM.BLLFactory;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using MySelf.QOSM.QuickOrderAPP.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml;

namespace MySelf.QOSM.QuickOrderAPP.ViewModels.Customer
{
    public class CustomerListViewModel : ListVMBase
    {
        ICustomerService customerService = InstanceFactory.CreateInstance<ICustomerService>();
        bool oldShowDel = false;
        public CustomerListViewModel()
        {
            ShowDeleted = false;
            ShowUnDel = true;
            PageInfoVM = new UControl.UPagerViewModel();
            PageInfoVM.PageSize = 20;
            PageInfoVM.PropertyChanged += (o, e) => LoadCustomerList();
            LoadCustomerList();
            InitCommands();
        }
        #region 属性


        private ObservableCollection<CustomerModel> customerList;

        public ObservableCollection<CustomerModel> CustomerList
        {
            get { return customerList; }
            set
            {
                customerList = value;
                OnPropertyChanged();
            }
        }
        private bool isClearAll;
        public bool IsClearAll
        {
            get { return isClearAll; }
            set
            {
                isClearAll = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region 命令
        //清除已注销客户命令      
        public ICommand ClearAllLogoffCustomersCmd { get; set; }
        //重置客户密码命令
        public ICommand ResetPwdCmd { get; set; }
        //启用冻结的客户账号
        public ICommand EnableCustomerCmd { get; set; }
        #endregion
        #region 方法

        private void InitCommands()
        {
            FindDataListCmd = new RelayCommand(o =>
            {
                IsClearAll = ShowDeleted;//清除按钮可用性-----未注销、主注销选择状态
                LoadCustomerList();
            });
            ResetPwdCmd = new RelayCommand(c =>
            {
                CustomerInfo cust = c as CustomerInfo;
                //重置密码处理---重置 为123456
                ResetCustPwd(cust);
            });
            EnableCustomerCmd = new RelayCommand(c =>
            {
                CustomerModel model = c as CustomerModel;
                EnableCustomer(model);
            });

            ClearAllLogoffCustomersCmd = new RelayCommand(o =>
            {
                ClearAllLogoffCustomers();
            });
        }
        //查询客户列表
        private void LoadCustomerList()
        {
            //调用查询方法
            ObservableCollection<CustomerModel> list = new ObservableCollection<CustomerModel>();
            if (HasDeleted != oldShowDel)
            {
                PageInfoVM.CurrentPage = 1;
                oldShowDel = HasDeleted;
            }
            PageModel<CustomerInfo> result = customerService.GetCustomerList(KeyWords, HasDeleted, PageInfoVM.StartIndex, PageInfoVM.PageSize);
            if (result != null)
            {
                List<CustomerInfo> custData = null;
                if (result.DataList.Count > 0)
                {
                    custData = result.DataList;
                    PageInfoVM.TotalCount = result.TotalCount;//总记录数
                    custData.ForEach(c => list.Add(new CustomerModel(c)));
                }
                else
                {
                    PageInfoVM.TotalCount = 0;
                }
            }
            CustomerList = list;
        }
        //客户密码重置处理
        private void ResetCustPwd(CustomerInfo cust)
        {
            string msgTtile = "重置密码";
            if (ShowQuestion("你确定要重置该客户的登录密码吗？", msgTtile) == MessageBoxResult.OK)
            {
                bool blSet = customerService.ResetCustomerPwd(cust);//调用重置密码方法
                if (blSet)
                {
                    ShowSuccess($"客户：{cust.CustomerName} 密码重置成功！", msgTtile);
                }
                else
                {
                    ShowError($"客户：{cust.CustomerName} 密码重置失败！", msgTtile);
                    return;
                }
            }
        }

        //启用客户
        private void EnableCustomer(CustomerModel model)
        {
            string msgTtile = "启用客户";
            if (ShowQuestion("你确定要启用该客户吗？", msgTtile) == MessageBoxResult.OK)
            {
                CustomerInfo cust = model.Customer;
                bool blSet = customerService.EnabledCustomer(cust);//调用启用方法
                if (blSet)
                {
                    ShowSuccess($"客户：{cust.CustomerName} 启用成功！", msgTtile);
                    //刷新客户信息----从数据库中读取出来（以前）  、 界面上更新信息，让它与数据库中数据一致
                    cust.CustomerState = true;
                    model.ShowEnable = Visibility.Collapsed;
                    model.ShowReset = Visibility.Visible;
                    model.Customer = cust;
                }
                else
                {
                    ShowError($"客户：{cust.CustomerName} 启用失败！", msgTtile);
                    return;
                }
            }
        }

        //清除所有已注销客户
        private void ClearAllLogoffCustomers()
        {
            string msgTtile = "清除已注销客户";
            if (ShowQuestion("你确定要清除所有的已注销客户信息吗？", msgTtile) == MessageBoxResult.OK)
            {
                bool blClear = customerService.ClearAllLogoffCustomer();//调用清除注销方法
                if (blClear)
                {
                    ShowSuccess("所有已注销客户清除成功！", msgTtile);
                    //刷新客户信息
                    CustomerList.Clear();
                }
                else
                {
                    ShowError("所有已注销客户清除失败！", msgTtile);
                    return;
                }
            }
        }
        #endregion
    }
}
