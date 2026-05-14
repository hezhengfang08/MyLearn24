using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class FeeAccess : WebAccess, IFeeAccess
    {
        public FeeAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetFeePage(string key, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/fee/page/{key}/{index}/{size}";
            return this.Get(uri);
        }

        public string GetFeeModes()
        {
            string uri = $"/api/fee/feemode";
            return this.Get(uri);
        }
    }
}
