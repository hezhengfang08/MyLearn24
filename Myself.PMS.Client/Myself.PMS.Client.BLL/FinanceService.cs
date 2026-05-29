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
    public class FinanceService : BaseService, IFinanceService
    {
        IFinanceAccess _financeAccess;
        public FinanceService(IFinanceAccess financeAccess)
        {
            _financeAccess = financeAccess;
        }
        public PageEntity<IEEntity[]> GetDatas(string key, DateTime start, DateTime end, int index, int size)
        {
            string json = _financeAccess.GetDatas(key,
                start.ToString("yyyy-MM-dd"),
                end.ToString("yyyy-MM-dd 23:59:59.999"),
                index, size);
            return this.GetResult<PageEntity<IEEntity[]>>(json);
        }

        public int ChangeState(int id, int state)
        {
            string json = _financeAccess.ChangeState(id, state);
            return this.GetResult<int>(json);
        }

        public int DeleteInfo(int id)
        {
            string json = _financeAccess.DeleteInfo(id);
            return this.GetResult<int>(json);
        }

        public int UpdateInfo(IEEntity entity)
        {
            string json = entity.Serialize();
            json = _financeAccess.UpdateInfo(json);
            return this.GetResult<int>(json);
        }
    }
}
