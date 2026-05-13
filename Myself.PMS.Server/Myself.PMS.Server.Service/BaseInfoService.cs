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
    public class BaseInfoService : IBaseInfoService
    {
        ISqlSugarClient _client;
        public BaseInfoService(ISqlSugarClient client)
        {
            _client = client;
        }

        public int CancelState(int id)
        {
            return _client.Updateable<BaseInfo>()
                .SetColumns(bi => new BaseInfo { state = -1 })
                .Where(bi => bi.InfoId == id)
                .ExecuteCommand();
        }

        public int DeleteBaseInfo(int id)
        {
            return _client.Deleteable<BaseInfo>().In(id).ExecuteCommand();
        }

        public BaseInfo[] GetBaseInfos(string key, int pageIndex, int pageSize, ref int totalCount)
        {
            return _client.Queryable<BaseInfo>()
                        .Where(bi =>
                            string.IsNullOrEmpty(key) ||
                            bi.InfoHeader.Contains(key) ||
                            bi.InfoContent.Contains(key) ||
                            bi.InfoKey.Contains(key))
                        // 按时间倒序排列
                        .OrderByDescending(bi => bi.ModifyTime)
                        // 分页操作
                        .ToPageList(pageIndex, pageSize, ref totalCount)

                        .ToArray();
        }

        public int PublishState(int id)
        {
            return _client.Updateable<BaseInfo>()
                 .SetColumns(bi => new BaseInfo
                 {
                     state = 1,
                     PublishTime = DateTime.Now
                 })
                 .Where(bi => bi.InfoId == id)
                 .ExecuteCommand();
        }

        public int RevokeState(int id)
        {
            return _client.Updateable<BaseInfo>()
                .SetColumns(bi => new BaseInfo { state = 0, PublishTime = null })
                .Where(bi => bi.InfoId == id)
                .ExecuteCommand();
        }

        public int UpdateBaseInfo(BaseInfo baseInfo)
        {
            if (baseInfo.InfoId == 0)
            {
                return _client.Insertable(baseInfo).ExecuteCommand();
            }
            else
            {
                return _client.Updateable(baseInfo).ExecuteCommand();
            }
        }
    }
}
