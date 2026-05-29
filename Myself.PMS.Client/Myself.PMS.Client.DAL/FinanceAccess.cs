using Myself.PMS.Client.Entities;
using Myself.PMS.Client.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Myself.PMS.Client.DAL
{
    public class FinanceAccess : WebAccess, IFinanceAccess
    {
        public FinanceAccess(GlobalValues globalValues) : base(globalValues)
        {
        }

        public string GetDatas(string key, string start, string end, int index, int size)
        {
            key = string.IsNullOrEmpty(key) ? "none" : key;
            string uri = $"/api/finance/page/{key}/{start}/{end}/{index}/{size}";
            return this.Get(uri);
        }

        public string ChangeState(int id, int state)
        {
            string uri = $"/api/finance/state/{id}/{state}";
            return this.Get(uri);
        }

        public string DeleteInfo(int id)
        {
            string uri = $"/api/finance/delete/{id}";
            return this.Get(uri);
        }

        public string UpdateInfo(string ieJson)
        {
            string uri = "/api/finance/update";

          
            return this.PostJson(uri, ieJson);
        }
    }
}
