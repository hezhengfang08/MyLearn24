using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IFeeService
    {
        PageEntity<FeeEntity[]> GetFeePage(string key, int index, int size);

        FeeModeEntity[] GetFeeModes();
        int UpdateFee(FeeEntity fee);

        int DeleteFee(int id);

        int ChangeState(int id, int state);
    }
}
