using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParing.IService
{
    public interface IAutoService : IBaseService
    {
        IEnumerable<AutoRegister> GetAutoList(string key, int pageSize, int pageIndex, out int totalCount);


    }
}
