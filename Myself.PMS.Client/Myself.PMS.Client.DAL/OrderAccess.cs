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

        public string ChangeState(string id, int state)
        {
            string uri = $"/api/order/state/{id}/{state}";
            return this.Get(uri);
        }

        public string DeleteOrder(string id)
        {
            string uri = $"/api/order/delete/{id}";
            return this.Get(uri);
        }

        public string GetOrders(string key, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/order/page/{key}/{index}/{size}";
            return this.Get(uri);
        }

        public string UpdateOrder(string orderJson)
        {
            string uri = "/api/order/update";
            return this.PostJson(uri, orderJson);
        }
    }
}
