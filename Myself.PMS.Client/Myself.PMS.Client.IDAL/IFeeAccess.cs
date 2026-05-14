using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IDAL
{
    public interface IFeeAccess
    {
        string GetFeePage(string key, int index, int size);

        string GetFeeModes();
        string UpdateFee(string feeJson);

        string DeleteFee(int id);

        string ChangeState(int id, int state);
    }
}
