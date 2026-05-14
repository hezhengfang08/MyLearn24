using Myself.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.IService
{
    public interface IFeeService
    {
        FeeEntity[] GetFees(string key, int pageIndex, int pageSize, ref int totalCount);

        FeeModeEntity[] GetFeeModes();
        int UpdateFee(FeeEntity feeEntity);

        int DeleteFee(int id);

        int ChangeFeeState(int id, int state);
    }
}
