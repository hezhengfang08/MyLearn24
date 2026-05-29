using Myself.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.IService
{
    public interface IContractService
    {
        ContractEntity[] GetDatas(string key, DateTime start, DateTime end, int index, int size, ref int totalCount);
        int UpdateInfo(ContractEntity contract);
        int DeleteInfo(int id);
        int ChangeState(int id, int state);
        int Execute(ContractEntity contract);
        int Archived(ContractEntity contract);
    }
}
