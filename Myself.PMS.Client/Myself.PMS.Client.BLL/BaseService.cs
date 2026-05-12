using Myself.PMS.Client.Entities;
using Myself.PMS.Client.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.BLL
{
    public class BaseService
    {
        public T GetResult<T>(string json)
        {
            var result = json.Deserialize<Result<T>>();

            if (result == null)
                throw new Exception("数据获取失败!");
            if (result.State != 200)
                throw new Exception(result.ExceptionMessage);

            return result.Data;
        }
    }
}
