using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class OrderAccess : WebAccess, IOrderAccess
    {
        public OrderAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetOrders(string key, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/order/page/{key}/{index}/{size}";
            return this.Get(uri);
        }
    }
}
