using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IBLL;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class BaseInfoService : BaseService, IBaseInfoService
    {
        IBaseInfoAccess _baseInfoAccess;
        public BaseInfoService(IBaseInfoAccess baseInfoAccess)
        {
            _baseInfoAccess = baseInfoAccess;
        }
        public PageEntity<BaseInfo[]> GetInfoPage(string key, int index, int size)
        {
            string json = _baseInfoAccess.GetInfoPage(key, index, size);
            return this.GetResult<PageEntity<BaseInfo[]>>(json);
        }
    }
}
