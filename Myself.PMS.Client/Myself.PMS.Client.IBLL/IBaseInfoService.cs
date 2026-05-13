using Myself.PMS.Client.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IBLL
{
    public interface IBaseInfoService
    {
        PageEntity<BaseInfo[]> GetInfoPage(string key, int index, int size);
        int UpdateInfo(BaseInfo info);

        int DeleteInfo(int id);

        int CancelState(int id);
        int PublishState(int id);
        int RevokeState(int id);
    }
}
