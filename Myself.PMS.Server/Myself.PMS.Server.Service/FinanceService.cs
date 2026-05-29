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
    public class FinanceService : IFinanceService
    {
        ISqlSugarClient _client;
        public FinanceService(ISqlSugarClient client)
        {
            _client = client;
        }
        public IEEntity[] GetDatas(string key, DateTime start, DateTime end, int index, int size, ref int totalCount)
        {
            // 时间是一定要判断
            // 有可能没有   
            return _client.Queryable<IEEntity>()
                     .Where(ie =>
                     ie.HappendTime >= start && ie.HappendTime <= end &&

                     (string.IsNullOrEmpty(key) ||
                     ie.AmountDesc.Contains(key) ||
                     ie.ProjectName.Contains(key))
                        )
                     .ToPageList(index, size, ref totalCount)
                     .ToArray();
        }

        public int ChangeState(int id, int state)
        {
            return _client.Updateable<IEEntity>()
                 .SetColumns(fe => new IEEntity { State = state })
                 .Where(oi => oi.IEID == id)
                 .ExecuteCommand();
        }

        public int DeleteInfo(int id)
        {
            return _client.Deleteable<IEEntity>().In(id).ExecuteCommand();
        }

        public int UpdateInfo(IEEntity entity)
        {
            int count = 0;
            if (entity.IEID == 0)
            {
                count = _client.Insertable(entity).ExecuteCommand();
            }
            else
            {
                count = _client.Updateable(entity).ExecuteCommand();
            }
            return count;
        }
    }
}
