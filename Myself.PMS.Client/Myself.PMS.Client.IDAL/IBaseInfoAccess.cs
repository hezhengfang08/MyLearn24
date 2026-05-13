using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IDAL
{
    public interface IBaseInfoAccess
    {
        string GetInfoPage(string key, int index, int size);
    }
}
