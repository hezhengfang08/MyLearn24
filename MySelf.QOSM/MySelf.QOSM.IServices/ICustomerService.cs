using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.IServices
{

    public interface ICustomerService
    
    {
        /// <summary>
        /// 客户登录
        /// </summary>
        /// <param name="customNo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        CustomerInfo CustLogin(string customNo, string password);
        bool ExistCustomerNo(string customerNo);    
        bool RegisterCustomerInfo(CustomerInfo customerInfo);
        PageModel<CustomerInfo> GetCustomerList(string keywords,bool showDel,int startIndex, int pageSize);
        bool EnabledCustomer(CustomerInfo customerInfo);
        bool ResetCustomerPwd(CustomerInfo customerInfo);

        bool ClearAllLogoffCustomer();
        CustomerInfo GetCustomerInfo(int customerId);

        bool UpdateCustomer(CustomerInfo customerInfo);

        bool LogoffCustomer(int customerId);
        bool UpdateCustomerPwd(int customerId,string newPwd);

    }
}
