using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using MySelf.QOSM.Common;
using MySelf.QOSM.IServices;
using MySelf.QOSM.Models.Entities;
using MySelf.QOSM.Models.UIModels;
using NPOI.HSSF.Record.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        public bool ClearAllLogoffCustomer()
        {
            var logOffCusts = Query<CustomerInfo>(c=>c.IsDeleted).ToList();
            return ExecuteTrans(() =>
            {
                logOffCusts.ForEach(c =>
                {
                    UnCommitDelete(c);
                });

            }, () => { Commit(); });
        }

        public CustomerInfo CustLogin(string customNo, string password)
        {
            string endPwd = GetMD5Str(password);
            var customers = Query<CustomerInfo>(c => c.CustomerNo == customNo && c.CustomerPwd == password && c.IsDeleted == false).ToList(); 
            if(customers.Any())
            {
                return customers.First();
            }
            return null;
        }

        public bool EnabledCustomer(CustomerInfo customerInfo)
        {
            throw new NotImplementedException();
        }

        public bool ExistCustomerNo(string customerNo)
        {
            return Query<CustomerInfo>(c=>c.CustomerNo == customerNo && !c.IsDeleted).Any();    
        }

        

        public CustomerInfo GetCustomerInfo(int customerId)
        {
            return Find<CustomerInfo>(customerId);
        }

        public PageModel<CustomerInfo> GetCustomerList(string keywords, bool showDel, int startIndex, int pageSize)
        {
            PageModel<CustomerInfo> res = new PageModel<CustomerInfo>();
            var customerList = Query<CustomerInfo>(c=>c.IsDeleted == showDel).ToList();
            if(customerList.Any()) { 
                    customerList = customerList.Where(c=>c.CustomerName.Contains(keywords) || c.CustomerNo.Contains(keywords)
                    ||c.Phone.Contains(keywords)||c.Address.Contains(keywords)).ToList();
                if (customerList.Any())
                {
                    res.DataList = customerList.Skip(startIndex).Take(pageSize).ToList();
                    res.TotalCount = customerList.Count();
                }
            }
            return res;
        }

        public bool LogoffCustomer(int customerId)
        {
            bool res = false;
            var customer = Find<CustomerInfo>(customerId);
            if(customer != null)
            {
                customer.IsLogin = false;
                customer.LogOffTime = DateTime.Now; 
                Update(customer);
                if(dbContext.Entry(customer).State == EntityState.Unchanged)
                {
                    res = true;
                    Detach(customer);
                }
            }
            return res;
        }

        public bool RegisterCustomerInfo(CustomerInfo customerInfo)
        {
            customerInfo.CustomerPwd = GetMD5Str(customerInfo.CustomerPwd);
            Insert(customerInfo);
            return customerInfo.CustomerId > 0? true : false;   
        }

        public bool ResetCustomerPwd(CustomerInfo customerInfo)
        {
            var res = false;
            if(customerInfo != null)
            {
                var settings = ConfigHelper.GetAppSettings<AppSettings>("AppSettings");
                string endPwd = GetMD5Str(settings.ResetPwd);
                customerInfo.CustomerPwd = endPwd;
                Update(customerInfo);
                if(dbContext.Entry(customerInfo).State == EntityState.Unchanged) {
                 Detach(customerInfo);
                    res = true;
                }
            }
            return res;
        }

        public bool UpdateCustomer(CustomerInfo customerInfo)
        {
            bool blUpdate = false;
            dbContext.Entry(customerInfo).State = EntityState.Modified;
            dbContext.Entry(customerInfo).Property("CustomerPwd").IsModified = false;//不改密码
            Commit();
            if (dbContext.Entry(customerInfo).State == EntityState.Unchanged)
            {
                blUpdate = true;
                Detach(customerInfo);
            }
            return blUpdate;
        }

        public bool UpdateCustomerPwd(int customerId, string newPwd)
        {
            bool res = false;
            var customer = Find<CustomerInfo>(customerId);
            if(customer != null)
            {
                string endPwd = GetMD5Str(newPwd);
                customer.CustomerPwd = endPwd;
                Update(customer);
                if(dbContext.Entry(customer).State == EntityState.Unchanged) {
                    res = true;
                    Detach(customer);
                }
            }
            return res;
        }
    }
}
