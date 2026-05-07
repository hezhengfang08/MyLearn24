using Myself.SmartParking.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.SmartParing.IService
{
    public interface IRechargeService:IBaseService
    {
        IEnumerable<MemberRecharge> GetRecharges(string skey, int pageSize, int pageIndex, out int totalCount);
    }
}
