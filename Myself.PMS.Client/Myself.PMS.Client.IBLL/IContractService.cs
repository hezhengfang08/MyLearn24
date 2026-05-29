using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IContractService
    {
        PageEntity<ContractEntity[]> GetDatas(string key, DateTime start, DateTime end, int index, int size);

        int UpdateInfo(ContractEntity entity);
        int DeleteInfo(int id);
        int ChangeState(int id, int state);

        int Execute(ContractEntity entity);
        int Archived(ContractEntity entity);
    }
}
