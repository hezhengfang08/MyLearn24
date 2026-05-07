using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class RecordService : BaseService, IRecordService
    {
        public RecordService(DbContext context) : base(context)
        {
        }

        public OrderInfo GetOrderByLicense(string licenseId)
        {
            return this.Query<OrderInfo>(o => o.AutoLicense == licenseId && o.State == 0)
                .FirstOrDefault();
        }
    }
}
