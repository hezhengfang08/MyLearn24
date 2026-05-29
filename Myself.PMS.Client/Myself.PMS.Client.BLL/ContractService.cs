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
    public class ContractService : BaseService, IContractService
    {
        IContractAccess _contractAccess;
        public ContractService(IContractAccess contractAccess)
        {
            _contractAccess = contractAccess;
        }
        public PageEntity<ContractEntity[]> GetDatas(string key, DateTime start, DateTime end, int index, int size)
        {
            string json = _contractAccess.GetDatas(key,
               start.ToString("yyyy-MM-dd"),
               end.ToString("yyyy-MM-dd 23:59:59.999"),
               index, size);
            return this.GetResult<PageEntity<ContractEntity[]>>(json);
        }

        public int ChangeState(int id, int state)
        {
            string json = _contractAccess.ChangeState(id, state);
            return this.GetResult<int>(json);
        }

        public int DeleteInfo(int id)
        {
            string json = _contractAccess.DeleteInfo(id);
            return this.GetResult<int>(json);
        }

        public int UpdateInfo(ContractEntity entity)
        {
            string json = entity.Serialize();
            json = _contractAccess.UpdateInfo(json);
            return this.GetResult<int>(json);
        }

        public int Execute(ContractEntity entity)
        {
            string json = entity.Serialize();
            json = _contractAccess.Execute(json);
            return this.GetResult<int>(json);
        }

        public int Archived(ContractEntity entity)
        {
            string json = entity.Serialize();
            json = _contractAccess.Archived(json);
            return this.GetResult<int>(json);
        }
    }
}
