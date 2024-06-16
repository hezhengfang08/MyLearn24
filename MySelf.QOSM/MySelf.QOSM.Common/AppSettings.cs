using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySelf.QOSM.Common
{
    public class AppSettings
    {
        public string SqlServerConnection { get; set; }
        public string ResetPwd { get; set; } = "123456";
        public  string ServicesName { get; set; }
       
        public ServicesMapping ServicesMapping { get; set; }    
    }
    public class ServicesMapping
    {
        public string IRoleService { get; set; }
        public string IUserService { get; set; }
        public string IMenuService { get; set; }
        public string ICustomerService { get; set; }
        public string IFoodTypeService { get; set; }
        public string IFoodService { get; set; }
        public string IOrderService { get; set; }
        public string IStatisticsService { get; set; }
    }
}
