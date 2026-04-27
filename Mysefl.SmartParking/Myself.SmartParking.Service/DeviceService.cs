using Microsoft.EntityFrameworkCore;
using Myself.SmartParing.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParking.Service
{
    public class DeviceService : BaseService, IDeviceService
    {
        public DeviceService(DbContext context) : base(context)
        {
        }
    }
}
