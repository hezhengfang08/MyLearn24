using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IFinanceService
    {
        PageEntity<IEEntity[]> GetDatas(string key, DateTime start, DateTime end, int index, int size);

        int UpdateInfo(IEEntity entity);
        int DeleteInfo(int id);
        int ChangeState(int id, int state);
    }
}
