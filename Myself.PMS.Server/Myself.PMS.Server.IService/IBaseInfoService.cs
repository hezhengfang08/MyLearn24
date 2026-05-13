using Myself.PMS.Server.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Server.IService
{
    public interface IBaseInfoService
    {
        BaseInfo[] GetBaseInfos(string key, int pageIndex, int pageSize, ref int totalCount);
        int UpdateBaseInfo(BaseInfo baseInfo);

        int DeleteBaseInfo(int id);

        int CancelState(int id);
        int PublishState(int id);
        int RevokeState(int id);
    }
}
