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
    public class FeeService : IFeeService
    {
        ISqlSugarClient _client;
        public FeeService(ISqlSugarClient client)
        {
            _client = client;
        }
        public FeeEntity[] GetFees(string key, int pageIndex, int pageSize, ref int totalCount)
        {
            return _client.Queryable<FeeEntity>()
                        .Where(fe =>
                            string.IsNullOrEmpty(key) ||
                            fe.FeeMode.Contains(key) ||
                            fe.BName.Contains(key) ||
                            fe.QName.Contains(key) ||
                            fe.Description.Contains(key) ||
                            fe.RoomNumber.Contains(key))
                        .OrderByDescending(bi => bi.ModifyTime)
                        .ToPageList(pageIndex, pageSize, ref totalCount)
                        .ToArray();
        }

        public FeeModeEntity[] GetFeeModes()
        {
            return _client.Queryable<FeeModeEntity>().ToArray();
        }

        public int UpdateFee(FeeEntity feeEntity)
        {
            if (feeEntity.FeeId == 0)
            {
                return _client.Insertable(feeEntity).ExecuteCommand();
            }
            else
            {
                return _client.Updateable(feeEntity).ExecuteCommand();
            }
        }

        public int DeleteFee(int id)
        {
            return _client.Deleteable<FeeEntity>().In(id).ExecuteCommand();
        }

        public int ChangeFeeState(int id, int state)
        {
            return _client.Updateable<FeeEntity>()
                .SetColumns(fe => new FeeEntity { State = state })
                .Where(fe => fe.FeeId == id)
                .ExecuteCommand();
        }
    }
}
