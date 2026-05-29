using Myself.PMS.Server.Entities;
using Myself.PMS.Server.IService;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.Service
{
    public class ContractService : IContractService
    {
        ISqlSugarClient _client;
        public ContractService(ISqlSugarClient client)
        {
            _client = client;
        }

        public ContractEntity[] GetDatas(string key, DateTime start, DateTime end, int index, int size, ref int totalCount)
        {
            return _client.Queryable<ContractEntity>()
                        .Where(ce =>
                            ce.SignTime >= start && ce.SignTime <= end
                            &&
                            (string.IsNullOrEmpty(key) ||
                            ce.ContractName.Contains(key) ||
                            ce.ContractNumber.Contains(key) ||
                            ce.Opposite.Contains(key))
                        )
                        .ToPageList(index, size, ref totalCount)
                        .ToArray();
        }

        public int ChangeState(int id, int state)
        {
            return _client.Updateable<ContractEntity>()
                .SetColumns(bi => new ContractEntity { State = state })
                .Where(bi => bi.ContractId == id)
                .ExecuteCommand();
        }

        public int DeleteInfo(int id)
        {
            return _client.Deleteable<ContractEntity>().In(id).ExecuteCommand();
        }

        public int UpdateInfo(ContractEntity contract)
        {
            int count = 0;
            if (contract.ContractId == 0)
            {
                count = _client.Insertable(contract).ExecuteCommand();
            }
            else
            {
                count = _client.Updateable(contract).ExecuteCommand();
            }
            return count;
        }

        public int Execute(ContractEntity contract)
        {
            return _client.Updateable<ContractEntity>()
                .SetColumns(bi => new ContractEntity
                {
                    State = contract.State,
                    ExcuteAmount = contract.ExcuteAmount
                })
                .Where(bi => bi.ContractId == contract.ContractId)
                .ExecuteCommand();
        }

        public int Archived(ContractEntity contract)
        {
            return _client.Updateable<ContractEntity>()
                .SetColumns(bi => new ContractEntity
                {
                    State = 3,
                    ArchivedTime = contract.ArchivedTime,
                    ArchivedUserId = contract.ArchivedUserId,
                    ArchivedUserName = contract.ArchivedUserName,
                })
                .Where(bi => bi.ContractId == contract.ContractId)
                .ExecuteCommand();
        }
    }
}
