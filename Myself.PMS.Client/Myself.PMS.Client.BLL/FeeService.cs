using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class FeeService : BaseService, IFeeService
    {
        IFeeAccess _feeAccess;
        public FeeService(IFeeAccess feeAccess)
        {
            _feeAccess = feeAccess;
        }
        public PageEntity<FeeEntity[]> GetFeePage(string key, int index, int size)
        {
            string json = _feeAccess.GetFeePage(key, index, size);
            return this.GetResult<PageEntity<FeeEntity[]>>(json);
        }

        public FeeModeEntity[] GetFeeModes()
        {
            string json = _feeAccess.GetFeeModes();
            return this.GetResult<FeeModeEntity[]>(json);
        }

        public int UpdateFee(FeeEntity fee)
        {
            string json = _feeAccess.UpdateFee(fee.Serialize());
            return this.GetResult<int>(json);
        }

        public int DeleteFee(int id)
        {
            return this.GetResult<int>(_feeAccess.DeleteFee(id));   
        }

        public int ChangeState(int id, int state)
        {
           return this.GetResult<int>(_feeAccess.ChangeState(id, state));
        }
    }
}
