using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.IDAL
{
    public interface IOrderAccess
    {
        string GetOrders(string key, int index, int size);
        string UpdateOrder(string orderJson);

        string DeleteOrder(string id);
        string ChangeState(string id, int state);
    }
}
